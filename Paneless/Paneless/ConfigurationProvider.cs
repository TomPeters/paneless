using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Paneless.Core.Config;

namespace Paneless
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private Configuration _configuration;
        public Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                    RefreshConfiguration();
                return _configuration;
            }
        }

        private void RefreshConfiguration()
        {
            string configurationJson = ConfigurationExists() ? ReadConfigurationFromFile() : ReadDefaultConfiguration();
            _configuration = DeserializeConfigurationFromJson(configurationJson);
        }

        // TODO
        private string ReadConfigurationFromFile()
        {
            throw new System.NotImplementedException();
        }

        private bool ConfigurationExists()
        {
            return false;
        }

        private string ReadDefaultConfiguration()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Paneless.DefaultConfiguration.json"))
                using (StreamReader reader = new StreamReader(stream))
                    return reader.ReadToEnd();
        }

        private Configuration DeserializeConfigurationFromJson(string configurationJson)
        {
            return JsonConvert.DeserializeObject<Configuration>(configurationJson);
        }
    }
}
