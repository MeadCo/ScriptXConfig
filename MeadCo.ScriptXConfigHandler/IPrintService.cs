using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    public interface IPrintService
    {
        /// <summary>
        /// The Subscription ID to use top connect to the service, may be blank
        /// </summary>
        Guid SubscriptionGuid { get; }

        /// <summary>
        /// Returns true if a service is defined, whether it is valid/works is a different question!
        /// </summary>
        bool IsAvailable { get; }

        /// <summary>
        /// Determine if the ScriptX.Print service is available and should be used
        /// for the agent.
        /// Note this will always return true for non IE agents, false for IE 7..10
        /// and dependent on whether the service IsAvailable for IE 11
        /// </summary>
        /// <param name="userAgent">The user agent string from the browser</param>
        /// <returns></returns>
        bool UseForAgent(string userAgent);

        /// <summary>
        /// The end point for printing html 
        /// </summary>
        Uri PrintHtmlService { get; }

        /// <summary>
        /// The end point for discovering details on the subscription
        /// </summary>
        Uri SubscriptionService { get; }
    }
}
