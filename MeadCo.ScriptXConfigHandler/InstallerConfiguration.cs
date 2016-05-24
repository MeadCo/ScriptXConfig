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
        /// <summary>
        ///     Provides the name and location of the installer cab file.
        /// </summary>
        [ConfigurationProperty("filename", DefaultValue = "~/content/bin/smsx.cab", IsRequired = true)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 1, MaxLength = 256)]
        public string FileName
        {
            get { return (string) this["filename"]; }
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
        }

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
            get { return (string) this["installhelper"]; }
            set { this["installhelper"] = value; }
        }

        [System.Configuration.ConfigurationProperty("manualinstallers")]
        [ConfigurationCollection(typeof(Installers), AddItemName = "installer")]
        public Installers ManualInstallers
        {
            get
            {
                object o = this["manualinstallers"];
                return o as Installers;
            }
        }

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
        /// <param name="scope">Install scope - ujser or machine</param>
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
            // find an entry that matches the version, scope and processor ...
            foreach (InstallerConfiguration installer in ManualInstallers)
            {
                if (installer.Scope == scope && installer.Processor == processor &&
                    (new Version(installer.Version.Replace(",", "."))) == version)
                {
                    return Url.ResolveUrl(installer.FileName);
                }
            }
            return string.Empty;
        }

        public string InstallHelperUrl => Url.ResolveUrl(InstallHelper);
    }
}