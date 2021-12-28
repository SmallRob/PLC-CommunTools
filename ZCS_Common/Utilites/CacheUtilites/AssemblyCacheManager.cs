using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ZCS_Common
{
    /// <summary>
    /// 程序集缓存
    /// </summary>
    public class AssemblyCacheManager
    {
        private Hashtable ObjectCacheStore = new Hashtable();

        private static AssemblyCacheManager instance = null;
        private static object locker = new Object();

        public static AssemblyCacheManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new AssemblyCacheManager();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 获得一个窗体
        /// </summary>
        /// <param name="assemblyName">窗体空间名称</param>
        /// <param name="formName">窗体名</param>
        /// <returns>窗体</returns>
        public Form GetForm(string assemblyName, string formName)
        {
            return this.GetForm(assemblyName, formName, null);
        }

        /// <summary>
        /// 获得一个接受传参数的窗体
        /// </summary>
        /// <param name="assemblyName">窗体空间名称</param>
        /// <param name="formName">窗体名</param>
        /// <param name="args">窗体构建参数</param>
        /// <returns>窗体</returns>
        public Form GetForm(string assemblyName, string formName, params object[] args)
        {
            Type type = this.GetType(assemblyName, formName);
            return (Form)Activator.CreateInstance(type, args);
        }
        /// <summary>
        /// 获得一个窗体
        /// </summary>
        /// <param name="assemblyName_formName">窗体完整名称</param>
        /// <returns>窗体</returns>
        public Form GetForm(string assemblyName_formName)
        {
            return (Form)GetFormArg(assemblyName_formName, null);
        }

        /// <summary>
        /// 获得一个接受传参数的窗体
        /// </summary>
        /// <param name="assemblyName_formName">窗体完整名称</param>
        /// <param name="args">窗体构建参数</param>
        /// <returns>窗体</returns>
        public Form GetFormArg(string assemblyName_formName, params object[] args)
        {
            int m = assemblyName_formName.LastIndexOf('.');
            int n = assemblyName_formName.IndexOf('|');
            int len = assemblyName_formName.Length;
            string assemblyName = n > 0 ? assemblyName_formName.Substring(0, n) : assemblyName_formName.Substring(0, m);
            string formName = n > 0 ? assemblyName_formName.Substring(n + 1, len - (n + 1)) : assemblyName_formName.Substring(m + 1, len - (m + 1));

            Type type = this.GetType(assemblyName, formName);
            Form frm = (Form)Activator.CreateInstance(type, args);
            return frm;
        }

        public Type GetType(string assemblyName, string name)
        {
            Assembly assembly = this.Load(assemblyName);
            Type type = assembly.GetType(assemblyName + "." + name, true, false);
            return type;
        }

        /// <summary>
        /// 加载 Assembly
        /// </summary>
        /// <param name="assemblyName">命名空间</param>
        /// <returns>Assembly</returns>
        public Assembly Load(string assemblyName)
        {
            Assembly assembly = null;
            if (this.ObjectCacheStore.ContainsKey(assemblyName))
            {
                assembly = (Assembly)this.ObjectCacheStore[assemblyName];
            }
            else
            {
                assembly = Assembly.Load(assemblyName);
                this.ObjectCacheStore.Add(assemblyName, assembly);
            }
            return assembly;
        }

        public void Add(string key, object storeObject)
        {
            if (!this.ObjectCacheStore.ContainsKey(key))
            {
                this.ObjectCacheStore.Add(key, storeObject);
            }
        }

        public object Retrieve(string key)
        {
            return this.ObjectCacheStore[key];
        }

        public bool IsContains(string key)
        {
            return this.ObjectCacheStore.ContainsKey(key);
        }
    }
}
