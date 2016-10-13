using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    public interface IBitsFinder
    {
        /// <summary>
        /// Obtains a list of available providers for the given scope (i.e. different processors)
        /// </summary>
        /// <param name="scope">required scope</param>
        /// <returns></returns>
        List<IBitsProvider> Find(InstallScope scope);

        /// <summary>
        /// obtains a list of availabled providers for the processor architecture (i.e. scopes the processor)
        /// </summary>
        /// <param name="processor">required processor</param>
        /// <returns></returns>
        List<IBitsProvider> Find(MachineProcessor processor);

        /// <summary>
        /// obtain a provider for the scope and processor.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="processor"></param>
        /// <returns></returns>
        IBitsProvider FindSingle(InstallScope scope, MachineProcessor processor);

        /// <summary>
        /// obtain the best provider for the scope and the user agent.
        /// The implementation will determine the x86/x64 processor and 
        /// IE version and return the best provider for that IE Version.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="userAgent">e.g. Request.ServerVariables["HTTP_USER_AGENT"]</param>
        /// <returns></returns>
        IBitsProvider FindSingle(InstallScope scope, string userAgent);
    }
}
