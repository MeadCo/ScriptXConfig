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
    public class Installers : ConfigurationElementCollection
    {
        public InstallerConfiguration this[int index]
        {
            get { return base.BaseGet(index) as InstallerConfiguration; }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new InstallerConfiguration this[string fileName]
        {
            get { return (InstallerConfiguration) BaseGet(fileName); }
            set
            {
                if (BaseGet(fileName) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(fileName)));
                }
                BaseAdd(value);
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new InstallerConfiguration();
        }

        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((InstallerConfiguration) element).FileName;
        }
    }

}
