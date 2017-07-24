using System;
using System.Configuration;

namespace MeadCo.ScriptX.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class UriValidatorAttribute : ConfigurationValidatorAttribute
    {
        public UriValidatorAttribute() : base()
        {
        }

        public override ConfigurationValidatorBase ValidatorInstance => new UriValidator();

    }
}
