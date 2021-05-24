using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace CommunTools.Common
{
    /// <summary>
    /// Attribute反射类
    /// </summary>
    public class AttributesContext<T>
    {
        /// <summary>
        ///存放AttributesContext 类中所有的当前错误信息
        /// </summary>
        private string ExceptionBug;

        /// <summary>
        /// 读取映射的Uri属性
        /// </summary>
        /// <param name="Info">自定义类型</param>
        /// <returns>返回映射的属性</returns>
        public string XUri(T info)
        {
            Type userAttu = info.GetType();
            try
            {
                FuncURIAttribute uri = (FuncURIAttribute)userAttu.GetCustomAttributes(false)[0];

                //在FuncURIAttribute中设置的是不容许多个特性,故取第1个
                return uri.FuncUri;
            }
            catch (ArgumentNullException e)
            {
                ExceptionBug = e.Message;
                return null;
            }
            catch (NotSupportedException e1)
            {
                ExceptionBug = e1.Message;
                return null;
            }
        }

        /// <summary>
        /// 返回定义类与枚举建立的分组名称
        /// </summary>
        /// <param name="Info">枚举类</param>
        /// <returns></returns>
        public string XGroup(T info)
        {
            StringBuilder xGroups = new StringBuilder();

            Type types = info.GetType();
            PropertyInfo[] typesPro = types.GetProperties();

            foreach (PropertyInfo pro in typesPro)
            {
                object[] attu = pro.GetCustomAttributes(false);

                //object objValue = pro.GetValue(info, null);
                //object objFieldName = (Object)pro.Name;
                //object[] classInfo = new object[2];

                //classInfo[0] = objFieldName;
                //classInfo[1] = objValue;

                foreach (Attribute aGroup in attu)
                {
                    if (aGroup is FuncGroupAttribute)
                    {
                        FuncGroupAttribute group = aGroup as FuncGroupAttribute;

                        xGroups.Append(group.GroupTag + "," + group.GroupName + ";");
                    }
                }
            }

            if (xGroups.Length == 0) return "";
            return xGroups.ToString().Trim(';');
        }
    }
}
