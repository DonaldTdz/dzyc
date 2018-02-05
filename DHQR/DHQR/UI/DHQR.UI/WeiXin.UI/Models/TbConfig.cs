using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace DHQR.UI.Models
{
    public static class TbConfig
    {
        public static string Get(string code)
        {
            var tbConfig = ConfigurationManager.GetSection("TopConfig") as NameValueCollection;
            if (tbConfig == null)
                throw new ConfigurationErrorsException("配置文件是否存在有效性[\"TopConfig\"]");
            return tbConfig.Get(code);
        }
    }
}