using System;
using System.Collections.Generic;

namespace ZCS_Common
{
    /// <summary>
    /// Enum转换类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumConvert<T>
    {
        /// <summary>
        /// 将EnumType转换为String
        /// </summary>
        /// <param name="_enumType"></param>
        /// <returns></returns>
        public static string ToTypeString(T _enumType)
        {
            return _enumType.ToString();
        }

        /// <summary>
        /// 将Enum值转换为实际名
        /// </summary>
        /// <param name="_enumVal"></param>
        /// <returns></returns>
        public static string ToName(System.Enum _enum)
        {
            return System.Enum.GetName(typeof(T), _enum);
        }

        /// <summary>
        /// 将String转换为EnumType
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static T ToEnum(string _value)
        {
            //把要转化的枚举用泛型来代替
            return (T)System.Enum.Parse(typeof(T), _value);
        }

        /// <summary>
        /// 将Enum转换为KeyValue字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> ToKeyVlaueList()
        {
            System.Type t = typeof(T);
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (object item in System.Enum.GetValues(t))
            {
                dic.Add((int)item, System.Enum.GetName(t, item));
            }
            return dic;
        }
    }
}
