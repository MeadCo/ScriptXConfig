using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    public enum ServiceConnector
    {
        None = 0,
        Cloud = 1, // this includes OnPremise
        Windows = 2
    }

    /// <summary>
    /// Describe the availability of ScriptX.Print Services - implementors define the 
    /// service they can describe - None  - its not available (so use Add-on), Cloud, or On Premise
    /// </summary>
    public interface IPrintService
    {
        /// <summary>
        /// Returns the service defined, whether it is valid/works is a different question!
        /// </summary>
        ServiceConnector Availability { get; }

        /// <summary>
        /// The Subscription/License GUID to use to connect to the service, may be blank for OnPremise
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        ///     The Revision of the license - applies to a workstation license, may be blank
        /// </summary>
        int Revision { get; }

        /// <summary>
        ///     the location of the license file - applies to a workstation license, may be blank
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Determine if the ScriptX.Print service is could/should be used
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
        /// The end point for discovering details on the license/subscription
        /// </summary>
        Uri LicenseService { get; }

        /// <summary>
        /// The end point for monitoring the service
        /// </summary>
        Uri MonitorService { get; }

        /// <summary>
        /// The end point for testing service availability
        ///     e.g. returns IAmCloud, IAmOnPremise, IAmOnWindowsPC
        /// </summary>
        Uri TestService { get; }
    }
}
