using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;

namespace ZCS_FormUI.Controls
{
    public abstract class DataGridViewConfig
    {
        private static readonly object writeFile = new object();
        private static XmlDocument xmlDoc = null;

        #region public static DataSet ReadDGVConfig(string username, string nodeName) 读取配置信息 返回Ds
        /// <summary>
        /// 读取配置信息 返回Ds
        /// </summary>
        /// <param name="username"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static DataSet ReadDGVConfig(string username, string nodeName,string userid)
        {
            lock (writeFile)
            {
                DataSet ds = new DataSet();
                try
                {
                    string fileName = userid+"DataGridViewColumnsConfig";
                    string directPath = string.Format(@"{0}\SetXml",
                                                      AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"\{0}.xml", fileName);
                    if (xmlDoc == null)
                    {
                        InitXml(directPath);
                    }
                    ds = ReadXmlUserSet(username, nodeName);
                }
                catch
                {

                    ds = null;
                }
                finally
                {
                    if (xmlDoc != null)
                    {
                        xmlDoc = null;
                    }
                }
                return ds;
            }
        } 
        #endregion
        #region private static DataSet ReadXmlUserSet(string username, string nodeName) 读取配置
        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        private static DataSet ReadXmlUserSet(string username, string nodeName)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (xmlDoc == null || string.IsNullOrEmpty(nodeName) || string.IsNullOrEmpty(username))
            {
                return null;
            }
            XmlNode xmlnode = xmlDoc.SelectSingleNode("DataGridViewConfig").SelectSingleNode(username);
            if (xmlnode == null)
            {
                return null;
            }
            XmlNode infonode = xmlnode.SelectSingleNode(nodeName);

            if (infonode == null)
            {
                return null;
            }
            XmlAttributeCollection xmlAttrbutes = infonode.Attributes;
            foreach (XmlAttribute xmlattrbute in xmlAttrbutes)
            {
                string colname = xmlattrbute.Name.Replace("2F", "/").Replace("28", "(").Replace("29", ")").Replace("20", " ");

                dt.Columns.Add(colname);
            }
            //行数据
            object[] row = new object[xmlAttrbutes.Count];
            for (int k = 0; k < xmlAttrbutes.Count; k++)
            {
                row[k] = xmlAttrbutes[k].Value;
            }
            dt.Rows.Add(row);
            if (null == dt)
            {
                dt = new DataTable();
            }
            ds.Tables.Add(dt);

            return ds;
        }
        #endregion     
        

        #region public static bool WriteDGVConfig(string username, string nodeName, DataTable dt) 写入配置信息
        /// <summary>
        /// 写入配置信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="nodeName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool WriteDGVConfig(string username, string nodeName, DataTable dt,string userid)
        {
            lock (writeFile)
            {
                bool iftrue = false; ;
                try
                {
                    string fileName = userid+"DataGridViewColumnsConfig";
                    string directPath = string.Format(@"{0}\SetXml",
                                                      AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"\{0}.xml", fileName);
                    if (xmlDoc == null)
                    {
                        InitXml(directPath);
                    }
                    iftrue = WriteXmlUserSet(username, nodeName, dt, directPath);
                }
                catch
                {
                    iftrue = false; ;
                }

                return iftrue;
            }
        } 
        #endregion
        #region private static bool WriteXmlUserSet(string username, string nodeName, DataTable dt, string xmlFilePath) 写入配置信息
        /// <summary>
        /// 写入配置信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="nodeName"></param>
        /// <param name="dt"></param>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        private static bool WriteXmlUserSet(string username, string nodeName, DataTable dt, string xmlFilePath)
        {
            try
            {
                XmlNode xeUser = null;
                if (xmlDoc == null || string.IsNullOrEmpty(nodeName) || string.IsNullOrEmpty(username))
                {
                    return false;
                }
                XmlNode rootNode = xmlDoc.SelectSingleNode("DataGridViewConfig");
                XmlNode xmlnode = rootNode.SelectSingleNode(username);
                XmlNode infonode = null;
                if (xmlnode == null)
                {
                    xeUser = xmlDoc.CreateElement(username);
                }
                else
                {
                    xeUser = xmlnode;
                    infonode = xmlnode.SelectSingleNode(nodeName);

                }
                if (infonode != null)
                {
                    xmlnode.RemoveChild(infonode);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    XmlElement xeRow = xmlDoc.CreateElement(nodeName);
                    foreach (DataColumn col in dt.Columns)
                    {
                        string colname=col.ColumnName.Replace("/","2F").Replace("(","28").Replace(")","29").Replace(" ","20");
                        xeRow.SetAttribute(colname, dr[col.ColumnName].ToString());
                    }
                    xeUser.AppendChild(xeRow);
                }

                rootNode.AppendChild(xeUser);
                xmlDoc.Save(xmlFilePath);
                return true;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region private static void InitXml(string filPath) 判断xml是否存在，给xmlDoc 赋值
        /// <summary>
        /// InitXml(string filPath) 判断xml是否存在，给xmlDoc 赋值
        /// </summary>
        /// <param name="filPath"></param>
        private static void InitXml(string filPath)
        {
            xmlDoc = !File.Exists(filPath) ? CreateDefaultXml(filPath) : ReadXML(filPath);
        } 
        #endregion

        #region  private static XmlDocument ReadXML(string xmlFilePath) 读取XML配置文件 返回 XmlDocument
        /// <summary>   
        /// 读取XML配置文件的参数设置，获取下载的TXT文件路径与上传的数据文件路径   
        /// </summary>   
        /// <returns></returns>   
        private static XmlDocument ReadXML(string xmlFilePath)
        {
            XmlDocument xmlDoc = null;
            try
            {
                xmlDoc = new XmlDocument();
                //读取XML配置文件   
                xmlDoc.Load(xmlFilePath);
            }
            catch (System.Exception e)
            {
                //throw (e);
                xmlDoc = null;
            }
            return xmlDoc;
        }
        #endregion

        #region private static XmlDocument CreateDefaultXml(string xmlFilePath) 创建一个默认的XML配置文件"
        /// <summary>   
        /// 创建一个默认的XML配置文件   
        /// </summary>   
        private static XmlDocument CreateDefaultXml(string xmlFilePath)
        {
            XmlDocument xmlDoc = null;
            try
            {
                xmlDoc = new XmlDocument();
                //创建XML文件描述   
                StringBuilder xmlstr = new StringBuilder();
                xmlstr.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                xmlstr.Append("<DataGridViewConfig>");
                xmlstr.Append("</DataGridViewConfig>");
                xmlDoc.LoadXml(xmlstr.ToString());

                //保存xml文件   
                xmlDoc.Save(xmlFilePath);


            }
            catch (System.Exception ex)
            {
                xmlDoc = null;
            }
            return xmlDoc;
        }
        #endregion       
    }
}
