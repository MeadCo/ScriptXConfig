using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    /// <summary>
    /// A collection of InstallerConfiguration elements.
    /// </summary>
    [ConfigurationCollection(typeof(InstallersCollection), AddItemName = "installer")]
    public class InstallersCollection : ConfigurationElementCollection
    {
        /// <summary>
        ///     Provides the action to be used to assist with installing this version.
        /// </summary>
        [ConfigurationProperty("installhelper", DefaultValue = "", IsRequired = false)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 256)]
        public string InstallHelper
        {
            get { return (string)this["installhelper"]; }
        }

        public InstallerConfiguration this[int index]
        {
            get { return base.BaseGet(index) as InstallerConfiguration; }
        }

        public string InstallHelperUrl => Url.ResolveUrl(InstallHelper);

        protected override ConfigurationElement CreateNewElement()
        {
            return new InstallerConfiguration { Parent = this };
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((InstallerConfiguration) element).FileName;
        }
    }

}
