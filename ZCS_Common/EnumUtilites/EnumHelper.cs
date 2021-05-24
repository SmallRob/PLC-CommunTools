using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ZCS_Common
{
    public static class EnumHelper
    {
        #region 枚举方法
        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="en">枚举类型</param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }

        /// <summary>
        /// 根据描述获取枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetEnumByDescription<T>(string description)
        {
            System.Reflection.FieldInfo[] fields = typeof(T).GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
                if (objs.Length > 0 && (objs[0] as DescriptionAttribute).Description == description)
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }

        /// <summary>
        /// 根据枚举获取枚举名字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetEnumName<T>(Enum en)
        {
            return Enum.GetName(typeof(T), en);
        }

        /// <summary>  
        /// 从枚举类型和它的特性读出并返回一个键值对  
        /// </summary>  
        /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>  
        /// <returns>键值对</returns>  
        public static NameValueCollection GetNVCFromEnumValue(Type enumType)
        {
            NameValueCollection nvc = new NameValueCollection();
            Type typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[]
            fields = enumType.GetFields();
            string strText = string.Empty;
            string strValue = string.Empty;
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    strValue = ((int)enumType.InvokeMember(
                    field.Name, BindingFlags.GetField, null,
                    null, null)).ToString();
                    object[] arr = field.GetCustomAttributes(
                    typeDescription, true);
                    if (arr.Length > 0)
                    {
                        DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                        strText = aa.Description;
                    }
                    else
                    {
                        strText = field.Name;
                    }
                    nvc.Add(strText, strValue);
                }
            }

            return nvc;
        }

        /// <summary>
        /// 将枚举转换为List
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IList<EnumListModel> GetEnumList(Type enumType)
        {
            //获取枚举值转list集合
            IList<EnumListModel> EnumModelList = new List<EnumListModel>();
            foreach (object item in Enum.GetValues(enumType))
            {
                //通过遍历枚举拿到遍历的枚举值，然后根据枚举值拿到对应的枚举名称
                EnumListModel EnumModel = new EnumListModel();
                EnumModel.EnumType = (Enum)Enum.Parse(enumType, item.ToString());
                EnumModel.EnumId = Convert.ToInt32(item);
                EnumModel.EnumName = item.ToString();
                EnumModel.EnumDescrip = GetEnumDescription(EnumModel.EnumType);
                EnumModelList.Add(EnumModel);
            }

            return EnumModelList;
        }

        #endregion

        /// <summary>
        /// 获取枚举类型的枚举个数
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static int NumberOfEnumValues(Type enumType)
        {
            return enumType.GetFields(BindingFlags.Public | BindingFlags.Static).Length;
        }

        /// <summary>
        /// 通过枚举对象获取枚举列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> GetEnumList<T>(this T value)
        {
            List<T> list = new List<T>();
            if (value is Enum)
            {
                int valData = Convert.ToInt32((T)Enum.Parse(typeof(T), value.ToString()));
                Array tps = Enum.GetValues(typeof(T));

                list.AddRange(from object tp in tps where (Convert.ToInt32((T)Enum.Parse(typeof(T), tp.ToString())) & valData) == valData select (T)tp);
            }

            return list;
        }

        /// <summary>
        /// Gets all items for an enum value.（通过枚举对象获取所有枚举）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>(this Enum value)
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }
    }
}
