using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using MeadCo.ScriptX.Convertors;

namespace MeadCo.ScriptX
{
    /// <summary>
    ///     Describes the location of the downloadable code, its version and the
    ///     helper action to be used to assist with installing the code.
    /// </summary>
    public class InstallerConfiguration : ConfigurationElement, IBitsProvider, IBitsFinder
    {
        /// <summary>
        /// The owning collection, if there is one. And if there is, searches should be refered
        /// to that collection.
        /// </summary>
        internal  InstallersCollection Parent { get; set; }

        /// <summary>
        ///     Provides the name and location of the installer cab file.
        /// </summary>
        [ConfigurationProperty("filename", DefaultValue = "~/content/MeadCo.ScriptX/installers/smsx.cab", IsRequired = true)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 1, MaxLength = 256)]
        public string FileName => (string) this["filename"];

        /// <summary>
        ///     Provides the name and location of the installer cab file.
        /// </summary>
        [ConfigurationProperty("manualfilename", DefaultValue = "~/content/MeadCo.ScriptX/installers/scriptx.msi", IsRequired = false)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 1, MaxLength = 256)]
        public string ManualFileName => (string)this["manualfilename"];

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
                return v;
            }
        }

        public string CodebaseVersion => Version.Replace(".",",");

        public Version GetVersion => new Version(Version.Replace(",","."));

        [ConfigurationProperty("scope", DefaultValue = InstallScope.Machine, IsRequired = false)]
        [TypeConverter(typeof(CaseInsensitiveEnumConfigConverter<InstallScope>))]
        public InstallScope Scope { get { return (InstallScope)this["scope"]; } }

        [ConfigurationProperty("processor", DefaultValue = MachineProcessor.x86, IsRequired = false)]
        [TypeConverter(typeof(CaseInsensitiveEnumConfigConverter<MachineProcessor>))]
        public MachineProcessor Processor { get { return (MachineProcessor)this["processor"]; } }


        /// <summary>
        ///     Provides the action to be used to assist with installing this version.
        /// </summary>
        [ConfigurationProperty("installhelper", DefaultValue = "", IsRequired = false)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 0, MaxLength = 256)]
        public string InstallHelper
        {
            get
            {
                string s = (string) this["installhelper"];
                if (Parent != null && string.IsNullOrEmpty(s)) return Parent.InstallHelper;
                return s;
            }
        }

        public string InstallHelperUrl => Url.ResolveUrl(InstallHelper);

        // Implementation of IMeadCoBinaryBitsProvider
        //

        /// <summary>
        /// Return the codebase for the version defined in config.
        /// </summary>
        public string CodeBase => $"{Url.ResolveUrl(FileName)}#Version={CodebaseVersion}";


        /// <summary>
        /// Returns the URL to download the manual installer for machine scope/x86 for the default version
        /// available from the implementation (store)
        /// </summary>
        public string ManualInstallerDownloadUrl => Url.ResolveUrl(ManualFileName);

        // IBitsFinder ...
        public List<IBitsProvider> Find(InstallScope scope)
        {
            if (Parent != null) return Parent.Find(scope);

            List<IBitsProvider> providers = new List<IBitsProvider>();
            if (scope == Scope) providers.Add(this);
            return providers;
        }

        public List<IBitsProvider> Find(MachineProcessor processor)
        {
            if (Parent != null) return Parent.Find(processor);

            List<IBitsProvider> providers = new List<IBitsProvider>();
            if (processor == Processor) providers.Add(this);
            return providers;
        }

        public List<IBitsProvider> Find(string userAgent)
        {
            if (Parent != null) return Parent.Find(userAgent);

            List<IBitsProvider> providers = new List<IBitsProvider>();
            if (Library.ProcessorFromAgent(userAgent) == Processor) providers.Add(this);
            return providers;
        }

        public IBitsProvider FindSingle(InstallScope scope, MachineProcessor processor)
        {
            if (Parent != null) return Parent.FindSingle(scope, processor);

            return scope == Scope && processor == Processor ? this : default(InstallerConfiguration);
        }

        public IBitsProvider FindSingle(InstallScope scope, string userAgent)
        {
            if (Parent != null) return Parent.FindSingle(scope, userAgent);

            return FindSingle(scope,Library.ProcessorFromAgent(userAgent));
        }
    }
}