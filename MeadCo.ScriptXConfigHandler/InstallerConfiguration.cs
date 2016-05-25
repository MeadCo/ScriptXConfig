using System;
using System.ComponentModel;
using System.Configuration;
using MeadCo.ScriptX.Convertors;

namespace MeadCo.ScriptX
{
    /// <summary>
    ///     Describes the location of the downloadable code, its version and the
    ///     helper action to be used to assist with installing the code.
    /// </summary>
    public class InstallerConfiguration : ConfigurationElement, IMeadCoBinaryBitsProvider
    {
        internal  InstallersCollection Parent { get; set; }

        /// <summary>
        ///     Provides the name and location of the installer cab file.
        /// </summary>
        [ConfigurationProperty("filename", DefaultValue = "~/content/bin/smsx.cab", IsRequired = true)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 1, MaxLength = 256)]
        public string FileName => (string) this["filename"];

        /// <summary>
        ///     Provides the name and location of the installer cab file.
        /// </summary>
        [ConfigurationProperty("manualfilename", DefaultValue = "~/content/bin/scriptx.msi", IsRequired = false)]
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
                return v.Replace('.', ',');
            }
        }

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
        public string CodeBase => CodeBaseFor(new Version(Version.Replace(",",".")));

        /// <summary>
        /// Return the codebase for the given version if requesting the install scope and processor defined in config
        /// </summary>
        /// <param name="version">Required version</param>
        /// <param name="scope">Install scope - user or machine</param>
        /// <param name="processor">Processor architecture</param>
        /// <returns></returns>
        public string CodeBaseFor(Version version,InstallScope scope=InstallScope.Machine,MachineProcessor processor=MachineProcessor.x86)
        {
            return (scope == Scope && processor == Processor) ? ($"{Url.ResolveUrl(FileName)}#Version={Version.ToString()}") : String.Empty;
        }

        /// <summary>
        /// Returns the URL to download the manual installer for machine scope/x86 for the default version
        /// available from the implementation (store)
        /// </summary>
        public string ManualInstallerDownloadUrl => ManualInstallerDownloadUrlFor(new Version(Version.Replace(",", ".")));

        /// <summary>
        /// Returns the URL to download the manual installer for the given version
        /// </summary>
        /// <param name="version"></param>
        /// <param name="scope"></param>
        /// <param name="processor"></param>
        /// <returns></returns>
        public string ManualInstallerDownloadUrlFor(Version version, InstallScope scope = InstallScope.Machine,
            MachineProcessor processor = MachineProcessor.x86)
        {
            return (scope == Scope && processor == Processor) ? Url.ResolveUrl(ManualFileName) : String.Empty;
        }

    }
}