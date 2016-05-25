using System;
using System.Configuration;

namespace MeadCo.ScriptX
{
    /// <summary>
    ///     Describes the elements available in the configuraton section
    /// </summary>
    public class ScriptXConfigurationHandler : ConfigurationSection
    {
        // 'clientinstaller' element
        [ConfigurationProperty("clientinstaller")]
        public InstallerConfiguration ClientInstaller
        {
            get
            {
                // if the clientinstaller (legacy) element is present (i.e. simple single installer definition)
                // use it, else return the first available from the installers collection
                //
                // Present is defined as non-default version (version is required).
                InstallerConfiguration c = this["clientinstaller"] as InstallerConfiguration;

                if (c != null && ((new Version(0,0,0,0)) != c.GetVersion) ) return c;

                var installers = ClientInstallers;
                if (installers != null && installers.Count > 0 )
                {
                    c = installers[0];
                }

                return c;
            }
        }

        // 'License' element
        [ConfigurationProperty("license")]
        public LicenseConfiguration License => (LicenseConfiguration) this["license"];

        [ConfigurationProperty("clientinstallers")]
        public InstallersCollection ClientInstallers
        {
            get
            {
                object o = this["clientinstallers"];
                return o as InstallersCollection;
            }
        }
    }
}