using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    public enum InstallScope
    {
        User,
        Machine
    }

    public enum MachineProcessor
    {
        x86,
        x64
    }

    public interface IBitsProvider
    {
        /// <summary>
        /// Returns the version of this installer.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Returns the codebase for the default version available from
        /// the implementation (store)
        /// </summary>
        string CodeBase { get; }

        /// <summary>
        /// Returns the URL to download the manual installer for machine scope/x86 for the default version
        /// available from the implementation (store)
        /// </summary>
        string ManualInstallerDownloadUrl { get; }

        /// <summary>
        /// Returns the scope for this installer 
        /// </summary>
        InstallScope Scope { get; }

        /// <summary>
        /// Returns the Processor Architecture for this install
        /// </summary>
        MachineProcessor Processor { get; }
    }

}
