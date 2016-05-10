// <copyright file="ConfigurationTest.cs" company="Mead and Co Ltd">Copyright © Mead and Co Ltd. 2016</copyright>

using System;
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

            Assert.AreEqual<string>("8,0,0,0", c.Version);
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

            Assert.AreEqual<string>("", c.InstallHelper);
        }

        [TestMethod]
        public void LicenseGuid()
        {
            LicenseConfiguration lic = Configuration.License;

            Assert.AreEqual<Guid>(new Guid("{55326F1D-876A-447F-BA96-1A68B8EFC288}"),lic.Guid);
        }

        [TestMethod]
        public void LicenseRevision()
        {
            LicenseConfiguration lic = Configuration.License;
            Assert.AreEqual<int>(10, lic.Revision);
        }

        [TestMethod]
        public void LicenseFile()
        {
            LicenseConfiguration lic = Configuration.License;
            Assert.AreEqual<string>("~/content/sxlic.mlf", lic.FileName);
        }

        [TestMethod]
        public void CheckIsLicensed()
        {
            LicenseConfiguration lic = Configuration.License;
            Assert.IsTrue(lic.IsLicensed);
        }

        [TestMethod]
        public void CheckIsPerUser()
        {
            LicenseConfiguration lic = Configuration.License;
            Assert.IsTrue(lic.PerUser);
        }

        [TestMethod]
        public void CheckCodebase()
        {
            IMeadCoBinaryBitsProvider provider = Configuration.ClientInstaller;
            string codebase = provider.CodeBase;

            Assert.AreEqual<string>(("/content/meadco.scriptx/installers/smsx.cab#Version=8,0,0,0"),codebase);
        }
    }
}
