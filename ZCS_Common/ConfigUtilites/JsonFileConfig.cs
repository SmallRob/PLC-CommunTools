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
    { /// <summary>
      /// 配置内容
      /// </summary>
        private NameValueCollection _configs = new NameValueCollection();
        /// <summary>
        /// 默认路径
        /// </summary>
        public string _configFile = System.AppDomain.CurrentDomain.BaseDirectory + "appsettings.json";

        /// <summary>
        /// 配置节点关键字
        /// </summary>
        public string _configSection = "AppSettings";
        public JsonFileConfig()
        {

        }
        public JsonFileConfig(string configFile, string configSection)
        {
            this._configFile = System.AppDomain.CurrentDomain.BaseDirectory + configFile;
            this._configSection = configSection;
            _configs = LoadJsonConfig(this._configFile, this._configSection);
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
                foreach (JProperty prop in config_object[_configSection])
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

        public NameValueCollection ConfigNameValue
        {
            get { return _configs; }
        }
    }
}
