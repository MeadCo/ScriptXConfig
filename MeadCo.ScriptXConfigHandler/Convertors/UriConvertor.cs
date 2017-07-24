using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX.Convertors
{
    /// <summary>
    /// Attempt to convert the string to a uri
    /// </summary>
    class UriConvertor : ConfigurationConverterBase
    {
        public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
        {
            return data == null ? new Uri("http://script.print.meadroid.com") : new Uri(data as string);
        }
    }
}
