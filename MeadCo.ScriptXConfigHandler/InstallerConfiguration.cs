using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    /// <summary>
    /// Describes the location of the downloadable code, its version and the
    /// helper action to be used to assist with installinhg the code.
    /// </summary>
    public class InstallerConfiguration : ConfigurationElement
    {
        /// <summary>
        /// Provides the name and location of the installer cab file.
        /// </summary>
        [ConfigurationProperty("filename", DefaultValue = "~/content/bin/smsx.cab", IsRequired = true)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 1, MaxLength = 256)]
        public String FileName
        {
            get { return (String)this["filename"]; }
            set { this["filename"] = value; }
        }

        /// <summary>
        /// Provides the ScriptX version 
        /// </summary>
        [ConfigurationProperty("version", DefaultValue = "0,0,0,0", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 7, MaxLength = 12)]
        public String Version
        {
            get
            {
                string v = (String)this["version"];
                return v.Replace('.', ',');
            }
            set { this["Version"] = value; }
        }

        /// <summary>
        /// Provides the action to be used to assist with installing this version.
        /// </summary>
        [ConfigurationProperty("installhelper", DefaultValue = "", IsRequired = false)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 256)]
        public String InstallHelper
        {
            get { return (String)this["installhelper"]; }
            set { this["installhelper"] = value; }
        }
    }
}
