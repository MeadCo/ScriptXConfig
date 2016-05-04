using System;
using System.Configuration;

namespace MeadCo.ScriptX
{
    /// <summary>
    ///     A license configuration section indicates that MeadCo Security Manager
    ///     is required to license the content and provide acess to advanced features.
    /// </summary>
    public class LicenseConfiguration : ConfigurationElement
    {
        /// <summary>
        ///     The unqiue license ID (GUID)
        /// </summary>
        [ConfigurationProperty("guid", IsRequired = true)]
        public Guid Guid
        {
            get
            {
                Guid guid;
                try
                {
                    guid = (Guid) this["guid"];
                }
                catch (Exception)
                {
                    guid = Guid.Empty;
                }

                return guid;
            }
            set { this["guid"] = value.ToString(); }
        }

        /// <summary>
        ///     The Revision of the license
        /// </summary>
        [ConfigurationProperty("revision", DefaultValue = "0", IsRequired = true)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 100, MinValue = 0)]
        public int Revision
        {
            get { return (int) this["revision"]; }
            set { this["revision"] = value; }
        }

        /// <summary>
        ///     the location of the license file
        /// </summary>
        [ConfigurationProperty("filename", DefaultValue = "~/content/sxlic.mlf", IsRequired = false)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()[]{};'\"|\\", MinLength = 1, MaxLength = 256)]
        public string FileName
        {
            get
            {
                try
                {
                    return (string) this["filename"];
                }
                catch (Exception)
                {
                    return "~/content/sxlic.mlf";
                }
            }
            set { this["filename"] = value; }
        }

        /// <summary>
        ///     Determines if the accepted license is to be cached per user or per machine
        /// </summary>
        [ConfigurationProperty("peruser", DefaultValue = true, IsRequired = false)]
        public bool PerUser
        {
            get { return (bool) this["peruser"]; }
            set { this["peruser"] = value; }
        }

        /// <summary>
        ///     Returns true if licensing is required -- in other words that a GUID is specified.
        /// </summary>
        public bool IsLicensed => !Guid.Empty.Equals(Guid);
    }
}