using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ZCS_Common
{
    public class SelectFileDialog
    {
        #region 反射方法
        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static Assembly GetAssembly(string name)
        {
            Assembly m_asmb = null;
            foreach (AssemblyName aN in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                if (aN.FullName.StartsWith(name))
                {
                    m_asmb = Assembly.Load(aN);
                    break;
                }
            }
            return m_asmb;
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static Type GetType(string typeName, Assembly assembly, string name)
        {
            Type type = null;
            string[] names = typeName.Split('.');

            if (names.Length > 0)
                type = assembly.GetType(name + "." + names[0]);

            for (int i = 1; i < names.Length; ++i)
            {
                type = type.GetNestedType(names[i], BindingFlags.NonPublic);
            }
            return type;
        }

        static object Call(object obj, string func, params object[] parameters)
        {
            return CallAs(obj.GetType(), obj, func, parameters);
        }

        static object CallAs(Type type, object obj, string func, params object[] parameters)
        {
            return type.GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(obj, parameters);
        }

        static object GetEnum(string typeName, string name, Assembly assembly, string name1)
        {
            Type type = GetType(typeName, assembly, name1);
            FieldInfo fieldInfo = type.GetField(name);
            return fieldInfo.GetValue(null);
        }

        static object New(string name, Assembly assembly, string name1, params object[] parameters)
        {
            Type type = GetType(name, assembly, name1);

            ConstructorInfo[] ctorInfos = type.GetConstructors();
            foreach (ConstructorInfo ci in ctorInfos)
            {
                try
                {
                    return ci.Invoke(parameters);
                }
                catch { }
            }

            return null;
        }
        #endregion

        #region 显示窗口方法
        /// <summary>
        /// 显示选择文件或选择文件夹的方法
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="dir">选择窗口的初始化目录</param>
        /// <param name="isFile">是否是选择文件</param>
        /// <param name="Filter">选择文件的过滤器</param>
        /// <param name="isMultiselect">是否允许多选</param>
        /// <returns></returns>
        static string[] ShowDialog(string title, string dir, bool isFile, string Filter, bool isMultiselect)
        {
            // 是否成功
            var flag = false;

            // 文件选择器
            var ofd = new OpenFileDialog();

            ofd.Title = title;
            ofd.InitialDirectory = dir;
            ofd.AddExtension = false;
            ofd.CheckFileExists = false;
            ofd.DereferenceLinks = true;
            ofd.Multiselect = isMultiselect;

            if (isFile)
            {
                if (Filter != "") ofd.Filter = Filter;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    flag = true;
                }
            }
            else
            {
                // 反射出文件夹选择器
                // 变量
                string m_ns = "System.Windows.Forms";
                Assembly m_asmb = GetAssembly(m_ns);

                // 反射修改界面
                uint num = 0;
                Type typeIFileDialog = GetType("FileDialogNative.IFileDialog", m_asmb, m_ns);
                object dialog = Call(ofd, "CreateVistaDialog");
                Call(ofd, "OnBeforeVistaDialog", dialog);

                uint options = (uint)CallAs(typeof(FileDialog), ofd, "GetOptions");
                options |= (uint)GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS", m_asmb, m_ns);
                CallAs(typeIFileDialog, dialog, "SetOptions", options);

                object pfde = New("FileDialog.VistaDialogEvents", m_asmb, m_ns, ofd);
                object[] parameters = new object[] { pfde, num };
                CallAs(typeIFileDialog, dialog, "Advise", parameters);
                num = (uint)parameters[1];
                try
                {
                    int num2 = (int)CallAs(typeIFileDialog, dialog, "Show", IntPtr.Zero);
                    flag = 0 == num2;
                }
                finally
                {
                    CallAs(typeIFileDialog, dialog, "Unadvise", num);
                    GC.KeepAlive(pfde);
                }
            }

            // 返回选择的路径
            return flag ? ofd.FileNames : null;
        }
        #endregion

        #region 选择文件方法
        /// <summary>
        /// 选择所有文件
        /// </summary>
        /// <returns>路径</returns>
        public static string[] SelectFile()
        {
            return ShowDialog("选择文件", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), true, "", true);
        }
        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="Filter">过滤器</param>
        /// <returns>路径</returns>
        public static string[] SelectFile(string Filter)
        {
            return ShowDialog("选择文件", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), true, Filter, true);
        }
        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="Filter">过滤器</param>
        /// <param name="Dir">默认打开的目录</param>
        /// <returns>路径</returns>
        public static string[] SelectFile(string Filter, string Dir)
        {
            //如果未指定默认目录，则默认为桌面
            if (string.IsNullOrEmpty(Dir)) Dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return ShowDialog("选择文件", Dir, true, Filter, true);
        }
        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="Filter">过滤器</param>
        /// <param name="isMultiselect">是否允许多选</param>
        /// <returns>路径</returns>
        public static string[] SelectFile(string title, string Filter, bool isMultiselect)
        {
            return ShowDialog(title, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), true, Filter, isMultiselect);
        }
        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="dir">初始目录</param>
        ///<param name="isMultiselect">是否允许多选</param>
        /// <returns>路径</returns>
        public static string[] SelectFile(string title, string dir, string Filter, bool isMultiselect)
        {
            return ShowDialog(title, dir, true, Filter, isMultiselect);
        }
        #endregion

        #region 选择文件夹方法
        /// <summary>
        /// 选择目录
        /// </summary>
        /// <returns>路径</returns>
        public static string[] SelectDir()
        {
            return ShowDialog("选择文件夹", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), false, "", true);
        }
        /// <summary>
        /// 选择目录
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>路径</returns>
        public static string[] SelectDir(string title, bool isMultiselect)
        {
            return ShowDialog(title, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), false, "", isMultiselect);
        }
        /// <summary>
        /// 选择目录
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="dir">初始目录</param>
        /// <param name="isMultiselect">是否允许多选</param>
        /// <returns>路径</returns>
        public static string[] SelectDir(string title, string dir, bool isMultiselect)
        {
            return ShowDialog(title, dir, false, "", isMultiselect);
        }
        #endregion
    }
}
