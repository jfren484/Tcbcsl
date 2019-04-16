using Microsoft.Win32;
using System;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
    [RouteArea("Admin")]
    [RoutePrefix("Advanced")]
    public class AdvancedController : Controller
    {
        [Route("FrameworkVersion")]
        public ActionResult FrameworkVersion()
        {
            var version = Get45or451FromRegistry();

            return View((object)version);
        }

        private static string Get45or451FromRegistry()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
                                                   .OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                int releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                return "Version: " + CheckFor45DotVersion(releaseKey);
            }
        }

        // Checking the version using >= will enable forward compatibility,  
        // however you should always compile your code on newer versions of 
        // the framework to ensure your app works the same. 
        private static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 461808) return "4.7.2 or later";
            if (releaseKey >= 461308) return "4.7.1";
            if (releaseKey >= 460798) return "4.7";
            if (releaseKey >= 394802) return "4.6.2";
            if (releaseKey >= 394254) return "4.6.1";
            if (releaseKey >= 393295) return "4.6";
            if ((releaseKey >= 379893)) return "4.5.2";
            if ((releaseKey >= 378675)) return "4.5.1";
            if ((releaseKey >= 378389)) return "4.5";

            // This code should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }
    }
}