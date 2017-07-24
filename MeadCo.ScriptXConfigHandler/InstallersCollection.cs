using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeadCo.ScriptX.Helpers;

namespace MeadCo.ScriptX
{
    /// <summary>
    /// A collection of InstallerConfiguration elements.
    /// </summary>
    [ConfigurationCollection(typeof(InstallersCollection), AddItemName = "installer")]
    public class InstallersCollection : ConfigurationElementCollection, IBitsFinder
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


        // IBitsFinder...
        public List<IBitsProvider> Find(InstallScope scope)
        {
            return (from InstallerConfiguration i in this where i.Scope == scope select i).Cast<IBitsProvider>().ToList();
        }

        public List<IBitsProvider> Find(MachineProcessor processor)
        {
            return (from InstallerConfiguration i in this where i.Processor == processor select i).Cast<IBitsProvider>().ToList();
        }

        public List<IBitsProvider> Find(string userAgent)
        {
            if (AgentParser.IsInternetExplorer(userAgent))
            {
                MachineProcessor processor = AgentParser.Processor(userAgent);
                Version v8 = new Version(8, 0);

                if (AgentParser.IsInternetExplorer11(userAgent))
                {
                    var providers =
                        (from InstallerConfiguration i in this
                            where i.Processor == processor && i.GetVersion >= v8
                            select i)
                        .Cast<IBitsProvider>()
                        .ToList();
                    if (providers.Any())
                        return providers;
                }
                else
                {
                    var providers =
                        (from InstallerConfiguration i in this
                            where i.Processor == processor && i.GetVersion < v8
                            select i)
                        .Cast<IBitsProvider>()
                        .ToList();
                    if (providers.Any())
                        return providers;
                }

                return Find(processor);
            }

            return new List<IBitsProvider>();
        }

        public IBitsProvider FindSingle(InstallScope scope, MachineProcessor processor)
        {
            return (from InstallerConfiguration i in this where i.Processor == processor && i.Scope == scope select i).Cast<IBitsProvider>().FirstOrDefault();
        }

        /// <summary>
        /// Find the best we can for the user agent
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public IBitsProvider FindSingle(InstallScope scope, string userAgent)
        {
            if (AgentParser.IsInternetExplorer(userAgent))
            {

                MachineProcessor processor = AgentParser.Processor(userAgent);
                Version v8 = new Version(8, 0);

                var providers = from InstallerConfiguration i in this
                    where i.Processor == processor && i.Scope == scope
                    select i;

                // If the user is using IE 11 then provide ScriptX v8 if we can
                if (AgentParser.IsInternetExplorer11(userAgent))
                {
                    IBitsProvider provider =
                        (from InstallerConfiguration i in providers where i.GetVersion >= v8 select i).FirstOrDefault();
                    if (provider != null)
                    {
                        return provider;
                    }
                }
                else
                {
                    // Not using IE 11, provide ScriptX 7.7 (prefered) or earlier if we can
                    IBitsProvider provider =
                        (from InstallerConfiguration i in providers where i.GetVersion < v8 select i).FirstOrDefault();
                    if (provider != null)
                    {
                        return provider;
                    }
                }

                // provide something, if we can
                return FindSingle(scope, processor);
            }

            return null;
        }
    }

}
