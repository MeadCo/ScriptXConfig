using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    public interface ILicenseProvider
    {
        Guid Guid { get;  }

        int Revision { get; }

        bool PerUser { get; }

        string FileName { get; }

        bool IsLicensed { get; }
    }
}
