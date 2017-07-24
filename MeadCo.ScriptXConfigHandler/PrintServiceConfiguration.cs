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
        [ConfigurationProperty("subscriptionguid", IsRequired = false)]
        public Guid SubscriptionGuid
        {
            get
            {
                Guid guid;
                try
                {
                    guid = (Guid)this["subscriptionguid"];
                }
                catch (Exception)
                {
                    guid = Guid.Empty;
                }

                return guid;
            }
            set { this["subscriptionguid"] = value.ToString(); }
        }

        /// <summary>
        ///     The service version to connect to (used to fabricate the endpoints)
        /// </summary>
        [ConfigurationProperty("version", DefaultValue = "0", IsRequired = true)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 100, MinValue = 0)]
        public int Version
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

        [ConfigurationProperty("usealways",IsRequired = false)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool UseAlways
        {
            get
            {
                return (bool)this["usealways"];
            }

            set { this["usealways"] = value; }
        }

        /// <summary>
        /// IsAvailable if we have a >0 api version
        /// </summary>
        public bool IsAvailable => Version > 0;

        public Uri PrintHtmlService => IsAvailable ? new Uri($"{Server.AbsoluteUri}api/v{Version}/printhtml") : null;

        public Uri SubscriptionService => IsAvailable ? new Uri($"{Server.AbsoluteUri}api/v{Version}/licensing") : null;

        public bool UseForAgent(string userAgent)
        {
            return !AgentParser.IsInternetExplorer(userAgent) ||
                   (UseAlways && AgentParser.IsInternetExplorer11(userAgent));

        }
    }
}
