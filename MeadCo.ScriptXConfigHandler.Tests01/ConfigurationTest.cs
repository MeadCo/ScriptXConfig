// <copyright file="ConfigurationTest.cs" company="Mead and Co Ltd">Copyright © Mead and Co Ltd. 2016</copyright>

using System;
using System.Collections.Generic;
using MeadCo.ScriptX;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeadCo.ScriptXConfigHandler.Tests
{
    [TestClass]
    [PexClass(typeof(Configuration))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ConfigurationTest
    {
        private const string ChromePhoneAgent =
            "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) CriOS/56.0.2924.75 Mobile/14E5239e Safari/602.1";

        private const string ChromeDesktopAgent =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";

        private const string InternetExplorer11x86Agent =
            "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; Touch; rv:11.0) like Gecko";

        private const string InternetExplorer8Agent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)";

        [TestMethod]
        public void InstallerVersionConfig()
        {
            InstallerConfiguration c = Configuration.ClientInstaller;

            Assert.AreEqual<string>("8.0.0.0", c.Version);
        }

        [TestMethod]
        public void InstallerCodebaseVersionConfig()
        {
            InstallerConfiguration c = Configuration.ClientInstaller;

            Assert.AreEqual<string>("8,0,0,0", c.CodebaseVersion);
        }

        [TestMethod]
        public void InstallerFile()
        {
            InstallerConfiguration c = Configuration.ClientInstaller;

            Assert.AreEqual<string>("~/content/meadco.scriptx/installers/smsx.cab", c.FileName);
        }

        [TestMethod]
        public void InstallerHelper()
        {
            InstallerConfiguration c = Configuration.ClientInstaller;

            Assert.AreEqual<string>("~/MeadCo.ScriptX/installHelper", c.InstallHelper);
        }


        //[TestMethod]
        //public void InstallerHelper2()
        //{         
        //    Assert.AreEqual<string>("", Configuration.ClientInstallers.InstallHelper);
        //}

        [TestMethod]
        public void LicenseGuid()
        {
            ILicenseProvider lic = Configuration.License;

            Assert.AreEqual<Guid>(new Guid("{55326F1D-876A-447F-BA96-1A68B8EFC288}"),lic.Guid);
        }

        [TestMethod]
        public void LicenseRevision()
        {
            ILicenseProvider lic = Configuration.License;
            Assert.AreEqual<int>(10, lic.Revision);
        }

        [TestMethod]
        public void LicenseFile()
        {
            ILicenseProvider lic = Configuration.License;
            Assert.AreEqual<string>("~/content/sxlic.mlf", lic.FileName);
        }

        [TestMethod]
        public void CheckIsLicensed()
        {
            LicenseConfiguration lic = Configuration.License;
            Assert.IsTrue(lic.IsLicensed);
        }

        [TestMethod]
        public void CheckLicenseIsPerUser()
        {
            ILicenseProvider lic = Configuration.License;
            Assert.IsTrue(lic.PerUser);
        }

        [TestMethod]
        public void CheckCodebase()
        {
            InstallerConfiguration c = Configuration.ClientInstaller;
            IBitsProvider provider = c;

            Assert.AreEqual<MachineProcessor>(MachineProcessor.x86, c.Processor);
            Assert.AreEqual<InstallScope>(InstallScope.Machine, c.Scope);

            string codebase = provider.CodeBase;

            // using default codebase which assumes scope of machine and x86

            Assert.AreEqual<string>(("/content/meadco.scriptx/installers/smsx.cab#Version=8,0,0,0"),codebase);
        }

        [TestMethod]
        public void CheckDefaultMachineProcessor()
        {
            InstallerConfiguration c = Configuration.ClientInstaller;

            Assert.AreEqual<MachineProcessor>(MachineProcessor.x86, c.Processor);
        }

        [TestMethod]
        public void CheckDefaultScope()
        {
            InstallerConfiguration c = Configuration.ClientInstaller;

            Assert.AreEqual<InstallScope>(InstallScope.Machine, c.Scope);
        }

        //[TestMethod]
        //public void CheckDefinedScope()
        //{
        //    InstallerConfiguration c = Configuration.ClientInstaller;

        //    Assert.AreEqual<InstallScope>(InstallScope.User, c.Scope);
        //}

        [TestMethod]
        public void TestInstallers()
        {
            InstallersCollection c = Configuration.ClientInstallers;

            Assert.AreEqual(5, c.Count);

            Assert.AreEqual<InstallScope>(InstallScope.Machine, c[0].Scope);
            Assert.AreEqual<MachineProcessor>(MachineProcessor.x86, c[0].Processor);

            Assert.AreEqual<InstallScope>(InstallScope.Machine, c[2].Scope);
            Assert.AreEqual<InstallScope>(InstallScope.User, c[1].Scope);

            Assert.AreEqual<MachineProcessor>(MachineProcessor.x86, c[1].Processor);
            Assert.AreEqual<MachineProcessor>(MachineProcessor.x64, c[2].Processor);
        }

        [TestMethod]
        public void ManualInstallerFile()
        {
            IBitsProvider provider = Configuration.ClientInstaller;

            Assert.AreEqual<string>("/content/meadco.scriptx/installers/ScriptX.msi", provider.ManualInstallerDownloadUrl);
        }

        [TestMethod]
        public void TestFindScope()
        {
            IBitsFinder finder = Configuration.ClientInstaller;

            Assert.IsNotNull(finder);

            List<IBitsProvider> providers = finder.Find(InstallScope.Machine);
            Assert.AreEqual(3,providers.Count);

            Assert.AreEqual(2,finder.Find(MachineProcessor.x64).Count);

            IBitsProvider provider = finder.FindSingle(InstallScope.Machine, MachineProcessor.x86);
            Assert.IsNotNull(provider);

            Assert.IsNotNull(finder.FindSingle(InstallScope.Machine, MachineProcessor.x64));

        }

        [TestMethod]
        public void TestFindAlternateInstallers()
        {
            IBitsFinder finder = Configuration.ClientInstaller;

            IBitsProvider provider = finder.FindSingle(InstallScope.Machine, InternetExplorer11x86Agent);
            Assert.AreEqual("8,0,0,0", provider.CodebaseVersion);

            provider = finder.FindSingle(InstallScope.Machine, InternetExplorer8Agent);
            Assert.AreEqual("7,7,20,0", provider.CodebaseVersion);
        }

        [TestMethod]
        public void TestFindOnAgent()
        {
            IBitsFinder finder = Configuration.ClientInstaller;

            List<IBitsProvider> providers =
                finder.Find(InternetExplorer11x86Agent);

            Assert.AreEqual(2,providers.Count);

            providers = finder.Find(InternetExplorer8Agent);

            Assert.AreEqual(1, providers.Count);
        }

        [TestMethod]
        public void TestFindOnChromeAgent()
        {
            IBitsFinder finder = Configuration.ClientInstaller;
            List<IBitsProvider> providers =
                finder.Find(ChromePhoneAgent);

            Assert.AreEqual(0,providers.Count);
        }

        [TestMethod]
        public void ServiceUse()
        {
            IPrintService service = Configuration.PrintService;

            if (service.Availability != ServiceConnector.None)
            {

                // defaults, whether the service is defined or not.
                Assert.IsTrue(service.UseForAgent(ChromePhoneAgent));
                Assert.IsTrue(service.UseForAgent(ChromeDesktopAgent));

                // never true for Internet Explorer agents < 11
                Assert.IsFalse(service.UseForAgent(InternetExplorer8Agent));

                // a version of 0 => the scriptx.print service isnt defined
                if (service.Availability == ServiceConnector.None)
                {
                    // so shouldnt be available to IE 11
                    Assert.IsFalse(service.UseForAgent(InternetExplorer11x86Agent));
                }
                else
                {
                    // Non zero => the service is avaible for IE 11.
                    Assert.IsTrue(service.UseForAgent(InternetExplorer11x86Agent));
                }

                Assert.AreEqual<Guid>(new Guid("{13598d2f-8724-467b-ae64-6e53e9e9f644}"), service.Guid);

                if (service.Availability == ServiceConnector.Windows)
                {
                    Assert.AreEqual<string>("warehouse", service.FileName);
                    Assert.AreEqual(5, service.Revision);
                }
            }
        }

        [TestMethod]
        public void ServiceEndPoints()
        {
            IPrintService service = Configuration.PrintService;

            if (service.Availability != ServiceConnector.None)
            {
                Assert.AreEqual(new Uri("https://scriptx.print-roadmap.meadroid.com/api/v1/printhtml"),
                    service.PrintHtmlService);

                Assert.AreEqual(new Uri("https://scriptx.print-roadmap.meadroid.com/api/v1/licensing"),
                    service.LicenseService);

                Assert.AreEqual(new Uri("https://scriptx.print-roadmap.meadroid.com/api/v1/monitor"),
                    service.MonitorService);

                Assert.AreEqual(new Uri("https://scriptx.print-roadmap.meadroid.com/api/v1/test"),
                    service.TestService);

            }
        }
    }
}
