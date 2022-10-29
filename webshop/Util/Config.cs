using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace webshop.Util
{
    interface IConfig
    {
        bool DevEnvironment { get; }
    }

    public class Config : IConfig
    {
        public bool DevEnvironment { get; private set; }

        public Config()
        {
            LoadAppSettings();
        }

        private void LoadAppSettings()
        {
            DevEnvironment = bool.Parse(ConfigurationManager.AppSettings["devEnvironment"]);
        }
    }
}