using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using MeadCo.ScriptX.Convertors;
using MeadCo.ScriptX.Helpers;
using MeadCo.ScriptX.Validators;

namespace MeadCo.ScriptX
{
    /// <summary>
    /// A PrintService section indicates that non IE browsers are to be supported by the ScriptX.Print service
    /// Optionally, the IE 11 browser will use the service as well. The service does not support earlier versions of IE 
    /// </summary>
    public class PrintServiceConfiguration : ConfigurationElement, IPrintService
    {
        /// <summary>
        ///     The unqiue subscription ID (GUID) (required for Cloud)
        /// </summary>
        [ConfigurationProperty("guid", IsRequired = true)]
        public Guid Guid
        {
            get
            {
                Guid guid;
                try
                {
                    guid = (Guid)this["guid"];
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
        [ConfigurationProperty("revision", DefaultValue = "0", IsRequired = false)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 100, MinValue = 0)]
        public int Revision
        {
            get { return (int)this["revision"]; }
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
                    return (string)this["filename"];
                }
                catch (Exception)
                {
                    return "~/content/sxlic.mlf";
                }
            }
            set { this["filename"] = value; }
        }


        /// <summary>
        ///     The service version to connect to (used to fabricate the endpoints)
        /// </summary>
        [ConfigurationProperty("version", DefaultValue = "0", IsRequired = true)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 100, MinValue = 0)]
        public int ApiVersion
        {
            get { return (int)this["version"]; }
            set { this["version"] = value; }
        }

        /// <summary>
        ///     the root url of the server providing the service - end points will be fabricated.
        /// </summary>
        [ConfigurationProperty("server", IsRequired = true)]
        [UriValidator()]
        [TypeConverter(typeof(UriConvertor))]
        public Uri Server
        {
            get
            {
                return (Uri)this["server"];
            }
            set { this["server"] = value; }
        }

        [ConfigurationProperty("notie11",IsRequired = false)]
        [TypeConverter(typeof(BooleanConverter))]
        // ReSharper disable once InconsistentNaming
        public bool NotIE11
        {
            get
            {
                return (bool)this["notie11"];
            }

            set { this["notie11"] = value; }
        }


        public bool UseForAgent(string userAgent)
        {
            return !AgentParser.IsInternetExplorer(userAgent) ||
                   (!NotIE11 && AgentParser.IsInternetExplorer11(userAgent) && Availability != ServiceConnector.None);
        }

        /// <summary>
        /// IsAvailable if we have a >0 api version
        /// </summary>
        public ServiceConnector Availability
        {
            get
            {
                if ( ApiVersion > 0)
                {
                    return FileName.Length > 0 ? ServiceConnector.Windows : ServiceConnector.Cloud;
                }

                return ServiceConnector.None;
            }
        }

        public Uri PrintHtmlService => Availability != ServiceConnector.None ? new Uri($"{Server.AbsoluteUri}api/v{ApiVersion}/printhtml") : null;

        public Uri LicenseService => Availability != ServiceConnector.None ? new Uri($"{Server.AbsoluteUri}api/v{ApiVersion}/licensing") : null;

        public Uri MonitorService => Availability != ServiceConnector.None ? new Uri($"{Server.AbsoluteUri}api/v{ApiVersion}/monitor") : null;

        public Uri TestService => Availability != ServiceConnector.None ? new Uri($"{Server.AbsoluteUri}api/v{ApiVersion}/test") : null;

    }
}
