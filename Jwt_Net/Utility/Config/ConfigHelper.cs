using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace Utility
{
    public  class ConfigHelper
    {
        public ConfigHelper()
        {
        }
        public static string GetConnectionString(string Name)
        { 
            return ConfigurationManager.ConnectionStrings[Name].ConnectionString; 
        }
        public static string GetConnectionString_ProviderName(string Name)
        {
            return ConfigurationManager.ConnectionStrings[Name].ProviderName;
        }

        public static string GetAppSeting(string Name)
        {
            return ConfigurationManager.AppSettings[Name];
        }

        public static List<string> GetIgnoreName()
        {
            List<string> list = new List<string>();
            NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("IgnoreGroup/Ignore");
            foreach(var item in config)
            {
                var value = config[item.ToString()];
                list.Add(value);
            } 
            return list;
        }
    }
}
