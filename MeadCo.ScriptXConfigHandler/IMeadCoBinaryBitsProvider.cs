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

    public interface IMeadCoBinaryBitsProvider
    {
        /// <summary>
        /// Returns the default version available from the store.
        /// </summary>
        string Version { get; }

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
        /// <param name="scope"></param>
        /// <param name="processor"></param>
        /// <returns></returns>
        string CodeBaseFor(Version version,InstallScope scope=InstallScope.Machine,MachineProcessor processor=MachineProcessor.x86);

        /// <summary>
        /// Returns the URL to download the manual installer for machine scope/x86 for the default version
        /// available from the implementation (store)
        /// </summary>
        string ManualInstallerDownloadUrl { get; }

        /// <summary>
        /// Returns the URL to download the manual installer for the given version
        /// </summary>
        /// <param name="version"></param>
        /// <param name="scope"></param>
        /// <param name="processor"></param>
        /// <returns></returns>
        string ManualInstallerDownloadUrlFor(Version version, InstallScope scope = InstallScope.Machine, MachineProcessor processor = MachineProcessor.x86);
    }
}
