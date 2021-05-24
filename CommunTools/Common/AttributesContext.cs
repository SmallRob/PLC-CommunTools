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
                MemberInfo[] memInfo = userAttu.GetMember(info.ToString());

                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(FuncURIAttribute), false);
                    if (attrs != null && attrs.Length > 0)

                        //在FuncURIAttribute中设置的是不容许多个特性,故取第1个
                        return ((FuncURIAttribute)attrs[0]).FuncUri;
                }
                return info.ToString();
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
        public string XGroup()
        {
            StringBuilder xGroups = new StringBuilder();

            Type types = typeof(T);
            object[] attru = types.GetCustomAttributes(false);

            foreach (Attribute aGroup in attru)
            {
                if (aGroup is FuncGroupAttribute)
                {
                    FuncGroupAttribute group = aGroup as FuncGroupAttribute;

                    xGroups.Append(group.GroupTag + "," + group.GroupName + ";");
                }
            }

            if (xGroups.Length == 0) return "";
            return xGroups.ToString().Trim(';');
        }
    }
}
