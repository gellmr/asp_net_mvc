using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace asp_net_mvc.Helpers
{
    public static class ApplicationHelper
    {
        public static string SiteImagesFolderPath(){
            return ConfigurationManager.AppSettings["siteImagesFolderPath"].ToString();
        }
    }
}