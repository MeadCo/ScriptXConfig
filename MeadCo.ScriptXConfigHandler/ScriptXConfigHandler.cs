using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    /// <summary>
    /// Describes the elements available in the configuraton section
    /// </summary>
    class ScriptXConfigHandler : ConfigurationSection
    {
        // 'clientinstaller' element
        [ConfigurationProperty("clientinstaller")]
        public InstallerConfiguration ClientInstaller
        {
            get { return (InstallerConfiguration)this["clientinstaller"]; }
            set { this["clientinstaller"] = value; }
        }

        // 'License' element
        [ConfigurationProperty("license")]
        public LicenseConfiguration License
        {
            get { return (LicenseConfiguration)this["license"]; }
            set { this["license"] = value; }
        }

    }
}
