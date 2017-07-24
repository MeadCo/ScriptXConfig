using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX.Validators
{
    /// <summary>
    /// Ensure the Uri is absolute and not file/unc
    /// </summary>
    internal class UriValidator : ConfigurationValidatorBase
    {
        public override void Validate(object value)
        {
            Uri uri = (Uri) value;

            if (!uri.IsAbsoluteUri)
            {
                throw new Exception("Uri must be absolute");
            }

            if (uri.IsFile || uri.IsUnc)
            {
                throw new Exception("Uri cannot be file or unc");
            }

        }

        public override bool CanValidate(Type type)
        {
            return type == typeof(Uri);
        }
    }
}
