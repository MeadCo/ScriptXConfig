using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    /// <summary>
    /// Some useful functions
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Determine the processor the browser agent in running on
        /// </summary>
        /// <param name="agent">The browser user agent string</param>
        /// <returns></returns>
        public static MachineProcessor ProcessorFromAgent(string agent)
        {
            bool isWin64 = agent.Contains("Win64");

            return isWin64 ? MachineProcessor.x64 : MachineProcessor.x86;
        }

        /// <summary>
        /// Determine if the user agent is for IE 11 (or later is MS do ever produce a later version)
        /// </summary>
        /// <param name="agent">The browser user agent string</param>
        /// <returns></returns>
        public static bool AgentIs11(string agent)
        {
            return agent.Contains("Trident") && agent.Contains("rv:11");
        }
    }
}
