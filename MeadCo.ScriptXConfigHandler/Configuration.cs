using System.Configuration;

namespace MeadCo.ScriptX
{
    //  helpers ... return default if no section is defined.

    // Use: 
    //  MeadCo.ScriptX.Configuration.License
    //  MeadCo.ScriptX.Configuration.ClientInstaller
    //  MeadCo.ScriptX.Configuration.ClientInstallers
    public class Configuration
    {
        public static LicenseConfiguration License => ConfigSection.License;

        // if the clientinstaller (legacy) element is present (i.e. simple single installer definition)
        // use it, else return the first available from the installers collection
        //
        // Present is defined as non-default version (version is required).

        public static InstallerConfiguration ClientInstaller => ConfigSection.ClientInstaller;

        public static InstallersCollection ClientInstallers => ConfigSection.ClientInstallers;

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