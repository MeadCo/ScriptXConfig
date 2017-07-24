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
        /// Determine if the ScriptX.Print service is available and should be used
        /// for the agent
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
