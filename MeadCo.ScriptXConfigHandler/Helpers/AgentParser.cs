namespace MeadCo.ScriptX.Helpers
{
    /// <summary>
    /// Some useful functions
    /// </summary>
    public class AgentParser
    {
        /// <summary>
        /// Determine the processor the browser agent in running on
        /// </summary>
        /// <param name="agent">The browser user agent string</param>
        /// <returns></returns>
        public static MachineProcessor Processor(string agent)
        {
            bool isWin64 = agent.Contains("Win64");

            return isWin64 ? MachineProcessor.x64 : MachineProcessor.x86;
        }

        /// <summary>
        /// Determine if the user agent is for IE 11 (or later if MS do ever produce a later version)
        /// </summary>
        /// <param name="agent">The browser user agent string</param>
        /// <returns></returns>
        public static bool IsInternetExplorer11(string agent)
        {
            return IsInternetExplorer(agent) && agent.Contains("rv:11");
        }

        /// <summary>
        /// Determine if the user agent is for IE 
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public static bool IsInternetExplorer(string agent)
        {
            return agent.Contains("Trident");
        }
    }
}
