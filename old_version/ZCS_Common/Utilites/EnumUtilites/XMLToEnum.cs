using System;
using System.Reflection;
using System.Xml.Linq;

namespace ZCS_Common
{
    public class XMLToEnum
    {
        /// <summary>
        /// 获取XML文档的根节点对象
        /// </summary>
        /// <param name="xmlpath">XML路径</param>
        /// <returns></returns>
        public static XElement GetRootElement(string xmlpath)
        {
            return XElement.Load(xmlpath);
        }

        /// <summary>
        /// 获取一个节点的内容
        /// </summary>
        /// <param name="root">根节点对象</param>
        /// <param name="nodeName">子节点名称</param>
        /// <returns></returns>
        public static string GetSingleNodeValue(XElement root, string nodeName)
        {
            XElement xElement = root.Element(nodeName);
            return xElement == null ? null : xElement.Value;
        }

        /// <summary>
        /// 加载XML文件，设置泛型的所有私有静态字段的值
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="loadpath">XML路径</param>
        public static void LoadGenericStaticXml<T>(string loadpath)
        {
            Type curtype = typeof(T);
            XElement root = GetRootElement(loadpath);
            FieldInfo[] fis = curtype.GetFields(BindingFlags.Static | BindingFlags.NonPublic);

            // 遍历，设置各字段的值
            foreach (FieldInfo item in fis)
            {
                string fieldstrval = GetSingleNodeValue(root, item.Name);
                if (item.FieldType.BaseType == typeof(Enum))
                {// 枚举类型
                    item.SetValue(null, Enum.Parse(item.FieldType, fieldstrval));
                }
                else
                {
                    object obj = Convert.ChangeType(fieldstrval, item.FieldType);
                    item.SetValue(null, obj);
                }
            }
        }
    }
}
