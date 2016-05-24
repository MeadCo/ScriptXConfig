using System.Configuration;

namespace MeadCo.ScriptX
{
    // Use: 
    //  MeadCo.ScriptX.Configuration.License
    //  MeadCo.ScriptX.Configuration.ClientInstaller
    public class Configuration
    {
        //  helpers ... return default if no section is defined.
        public static LicenseConfiguration License => ConfigSection.License;

        public static InstallerConfiguration ClientInstaller => ConfigSection.ClientInstaller;

        public static Installers ManualInstallers => ConfigSection.ClientInstaller.ManualInstallers;

        private static ScriptXConfigurationHandler ConfigSection
        {
            get
            {
                ScriptXConfigurationHandler sxc = null;

                try
                {
                    sxc = (ScriptXConfigurationHandler) ConfigurationManager.GetSection("meadco/scriptx");
                }
                finally
                {
                    if (sxc == null)
                    {
                        sxc = new ScriptXConfigurationHandler();
                    }
                }
                return sxc;
            }
        }
    }
}