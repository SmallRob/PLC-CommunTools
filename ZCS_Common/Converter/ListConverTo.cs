using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZCS_Common
{
    public static class ListConverTo
    {
        /// <summary>
        /// List转成DataTable（支持匿名类型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> list)
        {
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");

            //创建传入对象名称的列
            foreach (var item in list.FirstOrDefault().GetType().GetProperties())
            {
                dt.Columns.Add(item.Name);
            }
            //循环存储
            foreach (var item in list)
            {
                //新加行
                DataRow value = dt.NewRow();
                //根据DataTable中的值，进行对应的赋值
                foreach (DataColumn dtColumn in dt.Columns)
                {
                    int i = dt.Columns.IndexOf(dtColumn);
                    //基元元素，直接复制，对象类型等，进行序列化
                    if (value.GetType().IsPrimitive)
                    {
                        value[i] = item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item, null);
                    }
                    else
                    {
                        value[i] = JsonConvert.SerializeObject(item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item, null));
                    }
                }
                dt.Rows.Add(value);
            }
            return dt;
        }
    }
}
