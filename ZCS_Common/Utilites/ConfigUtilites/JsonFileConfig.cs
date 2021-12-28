using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCS_Common
{
    public class JsonFileConfig
    {
        #region config
        /// <summary>
        /// 配置内容
        /// </summary>
        private NameValueCollection _configs = new NameValueCollection();

        /// <summary>
        /// 默认路径
        /// </summary>
        private string _configFile = System.AppDomain.CurrentDomain.BaseDirectory + "appsettings.json";

        /// <summary>
        /// 配置节点关键字
        /// </summary>
        private string _configSection = "AppSettings";


        public NameValueCollection ConfigNameValue
        {
            get { return _configs; }
        }

        public string ConfigSection { get => _configSection; set => _configSection = value; }

        public string ConfigFile { get => _configFile; set => _configFile = value; }

        public JsonFileConfig()
        {
        }
        #endregion

        /// <summary>
        /// 获取指定文件的配置
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="configSection"></param>
        public JsonFileConfig(string configFile, string configSection)
        {
            this.ConfigFile = System.AppDomain.CurrentDomain.BaseDirectory + configFile;
            this.ConfigSection = configSection;
            _configs = LoadJsonConfig(this.ConfigFile, this.ConfigSection);
        }

        /// <summary>
        /// 读取配置文件内容
        /// </summary>
        public NameValueCollection LoadJsonConfig(string _configFilePath, string configSection)
        {
            var configColltion = new NameValueCollection();
            JObject config_object = LoadJsonFile(_configFilePath);
            if (config_object == null || !(config_object is JObject))
                return null;

            if (config_object[configSection] != null)
            {
                foreach (JProperty prop in config_object[ConfigSection])
                {
                    configColltion[prop.Name] = prop.Value.ToString();
                }
            }

            return configColltion;
        }

        /// <summary>
        /// 解析Json文件
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public JObject LoadJsonFile(string FilePath)
        {
            JObject config_object = null;
            try
            {
                StreamReader sr = new StreamReader(FilePath, Encoding.UTF8);
                config_object = JObject.Parse(sr.ReadToEnd());
                sr.Close();
                return config_object;
            }
            catch
            {
                return null;
            }
        }

    }
}
