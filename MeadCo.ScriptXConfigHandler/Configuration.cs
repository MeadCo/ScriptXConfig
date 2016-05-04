using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    // Use: 
    //  MeadCo.ScriptX.Configuration.License
    //  MeadCo.ScriptX.Configuration.ClientInstaller
    class Configuration
    {
        //  helpers ... return default if no section is defined.
        public static LicenseConfiguration License => ConfigSection.License;

        public static InstallerConfiguration ClientInstaller => ConfigSection.ClientInstaller;

        private static ScriptXConfigHandler ConfigSection
        {
            get
            {
                ScriptXConfigHandler sxc;

                try
                {
                    sxc = (ScriptXConfigHandler)ConfigurationManager.GetSection("meadco/scriptx");
                }
                catch (Exception)
                {
                    sxc = new ScriptXConfigHandler();
                }
                return sxc;
            }
        }

    }
}
