using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    public interface IMeadCoBinaryBitsProvider
    {
        /// <summary>
        /// Returns the codebase for the default version available from
        /// the implementation (store)
        /// </summary>
        string CodeBase { get; }

        /// <summary>
        /// Returns the codebase for the given version, if available
        /// from the implementation (store)
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        string CodeBaseFor(Version version);

        string InstallHelper { get; }
    }
}
