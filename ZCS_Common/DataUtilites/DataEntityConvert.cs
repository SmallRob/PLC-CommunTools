using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ZCS_Common.DataUtilites
{
    /// <summary>
    /// 将DataRow/DataTable转换成Entity/Entity列表
    /// </summary>
    public static class DataEntityConvert<T> where T : new()
    {
        #region 将DataTable转换成Entity
        /// <summary>
        /// 将DataRow行转换成Entity(有问题)
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T ToEntity(DataRow dr)
        {
            T entity = new T();
            Type info = typeof(T);
            MemberInfo[] members = info.GetMembers();
            foreach (MemberInfo mi in members)
            {
                if (mi.MemberType == MemberTypes.Property)
                {
                    //读取属性上的DataField特性
                    object[] attributes = mi.GetCustomAttributes(typeof(DataFieldAttribute), true);
                    foreach (object attr in attributes)
                    {
                        DataFieldAttribute dataFieldAttr = attr as DataFieldAttribute;
                        if (dataFieldAttr != null)
                        {
                            PropertyInfo propInfo = info.GetProperty(mi.Name);
                            if (dr.Table.Columns.Contains(dataFieldAttr.ColumnName))
                            {
                                //根据ColumnName，将dr中的相对字段赋值给Entity属性
                                propInfo.SetValue(entity,
                                                  Convert.ChangeType(dr[dataFieldAttr.ColumnName], propInfo.PropertyType),
                                                  null);
                            }

                        }
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 将DataTable转换成Entity列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList(DataTable dt)
        {
            List<T> list = new List<T>(dt.Rows.Count);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(ToEntity(dr));
            }
            return list;
        }
        #endregion

        public static T DataTableToModel(DataTable dt)
        {
            // 定义实体
            T t = new T();

            // 获得此模型的类型
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {

                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite)
                        {
                            continue;
                        }

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                break;
            }
            return t;
        }

        #region Model转DataTable
        /// <summary>
        /// model转换DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ModelToDataTable<T>(T items)
        {
            DataTable tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            object[] values = new object[props.Length];
            for (int i = 0; i < props.Length; i++)
            {
                values[i] = props[i].GetValue(items, null);
            }

            tb.Rows.Add(values);
            return tb;
        }

        /// <summary>
        /// model转换DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ModelToDataByAttributes<T>(T items)
        {
            DataTable tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<PropertyInfo> lstPropsCurrent = new List<PropertyInfo>();

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);

                object[] attributes = prop.GetCustomAttributes(typeof(DataFieldAttribute), true);
                foreach (object attr in attributes)
                {
                    DataFieldAttribute dataFieldAttr = attr as DataFieldAttribute;
                    if (dataFieldAttr != null)
                    {
                        if (!tb.Columns.Contains(dataFieldAttr.ColumnName))
                        {
                            tb.Columns.Add(dataFieldAttr.ColumnName, t);

                            lstPropsCurrent.Add(prop);
                        }
                    }
                }
            }

            object[] values = new object[lstPropsCurrent.Count];
            for (int i = 0; i < lstPropsCurrent.Count; i++)
            {
                values[i] = lstPropsCurrent[i].GetValue(items, null);
            }

            tb.Rows.Add(values);
            return tb;
        }
        #endregion

        /// <summary>
        /// 根据实体类得到表结构
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable CreateData<T>(T entity) where T : new()
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
            }
            return dataTable;
        }

        /// <summary>
        /// 通过反射将DataTable转换为List泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToListByReflect<T>(DataTable dt) where T : new()
        {
            List<T> ts = new List<T>();
            string tempName = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    //tempName = pi.Name;
                    //if (dt.Columns.Contains(tempName))

                    object[] attributes = pi.GetCustomAttributes(typeof(DataFieldAttribute), true);
                    foreach (object attr in attributes)
                    {
                        DataFieldAttribute dataFieldAttr = attr as DataFieldAttribute;
                        if (dataFieldAttr != null)
                        {
                            object value = dr[String.Format("{0}", dataFieldAttr.ColumnName)];
                            if (value != DBNull.Value)
                            {
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 通过动态代理将DataTable转换为List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToListByEmit<T>(DataTable dt) where T : class, new()
        {
            List<T> list = new List<T>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return list;
            }

            DataTableEntityBuilder<T> eblist = DataTableEntityBuilder<T>.CreateBuilder(dt.Rows[0]);
            foreach (DataRow info in dt.Rows)
            {
                list.Add(eblist.Build(info));
            }

            dt.Dispose();
            dt = null;
            return list;
        }

        /// <summary>
        /// List泛型转换DataTable.
        /// </summary>
        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            List<PropertyInfo> lstPropsCurrent = new List<PropertyInfo>();
            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);

                object[] attributes = prop.GetCustomAttributes(typeof(DataFieldAttribute), true);
                foreach (object attr in attributes)
                {
                    DataFieldAttribute dataFieldAttr = attr as DataFieldAttribute;
                    if (dataFieldAttr != null)
                    {
                        if (!tb.Columns.Contains(dataFieldAttr.ColumnName))
                        {
                            tb.Columns.Add(dataFieldAttr.ColumnName, t);

                            lstPropsCurrent.Add(prop);
                        }
                    }
                }
            }

            foreach (T item in items)
            {
                if (lstPropsCurrent != null && lstPropsCurrent.Count > 0)
                {
                    object[] values = new object[lstPropsCurrent.Count]; /*props.Length*/

                    for (int i = 0; i < lstPropsCurrent.Count; i++)
                    {
                        values[i] = lstPropsCurrent[i].GetValue(item, null);
                    }

                    tb.Rows.Add(values);
                }
            }
            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        /// <summary>
        /// DataTable转换泛型List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList(DataTable dt)
        {
            // 定义集合
            List<T> ts = new List<T>();

            // 获得此模型的类型
            Type type = typeof(T);
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite)
                        {
                            continue;
                        }

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
