// <copyright file="ConfigurationTest.cs" company="Mead and Co Ltd">Copyright © Mead and Co Ltd. 2016</copyright>

using System;
using System.Collections.Generic;
using MeadCo.ScriptX;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeadCo.ScriptX.Tests
{
    [TestClass]
    [PexClass(typeof(Configuration))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ConfigurationTest
    {
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

            Assert.AreEqual(4, c.Count);

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
            Assert.AreEqual(2,providers.Count);

            Assert.AreEqual(2,finder.Find(MachineProcessor.x64).Count);

            IBitsProvider provider = finder.FindSingle(InstallScope.Machine, MachineProcessor.x86);
            Assert.IsNotNull(provider);

            Assert.IsNotNull(finder.FindSingle(InstallScope.Machine, MachineProcessor.x64));
        }
    }
}
