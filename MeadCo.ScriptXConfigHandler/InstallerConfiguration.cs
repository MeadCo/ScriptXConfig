using System;
using System.Configuration;

namespace MeadCo.ScriptX
{
    /// <summary>
    ///     Describes the location of the downloadable code, its version and the
    ///     helper action to be used to assist with installinhg the code.
    /// </summary>
    public class InstallerConfiguration : ConfigurationElement, IMeadCoBinaryBitsProvider
    {
        /// <summary>
        ///     Provides the name and location of the installer cab file.
        /// </summary>
        [ConfigurationProperty("filename", DefaultValue = "~/content/bin/smsx.cab", IsRequired = true)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 1, MaxLength = 256)]
        public string FileName
        {
            get { return (string) this["filename"]; }
            set { this["filename"] = value; }
        }

        /// <summary>
        ///     Provides the ScriptX version
        /// </summary>
        [ConfigurationProperty("version", DefaultValue = "0,0,0,0", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 7, MaxLength = 12)]
        public string Version
        {
            get
            {
                var v = (string) this["version"];
                return v.Replace('.', ',');
            }
            set { this["Version"] = value; }
        }

        /// <summary>
        ///     Provides the action to be used to assist with installing this version.
        /// </summary>
        [ConfigurationProperty("installhelper", DefaultValue = "", IsRequired = false)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 256)]
        public string InstallHelper
        {
            get { return (string) this["installhelper"]; }
            set { this["installhelper"] = value; }
        }

        public string CodeBase => CodeBaseFor(new Version(Version.Replace(",",".")));

        public string CodeBaseFor(Version version) => ($"{Url.ResolveUrl(FileName)}#Version={Version.ToString()}");

        public Uri InstallHelperUri
        {
            get { return null; }
        }
    }
}