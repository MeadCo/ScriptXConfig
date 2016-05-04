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
            get { return (InstallerConfiguration) this["clientinstaller"]; }
            set { this["clientinstaller"] = value; }
        }

        // 'License' element
        [ConfigurationProperty("license")]
        public LicenseConfiguration License
        {
            get { return (LicenseConfiguration) this["license"]; }
            set { this["license"] = value; }
        }
    }
}