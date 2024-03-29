// <copyright file="CapitalizeServiceTest.cs">Copyright ©  2017</copyright>
using System;
using Linkedin.CapitalizeText.Services;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linkedin.CapitalizeText.Services.Tests
{
    /// <summary>This class contains parameterized unit tests for CapitalizeService</summary>
    [PexClass(typeof(CapitalizeService))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class CapitalizeServiceTest
    {
        /// <summary>Test stub for CapitelizeText(String)</summary>
        [PexMethod]
        public string CapitelizeTextTest([PexAssumeUnderTest]CapitalizeService target, string text)
        {
            string result = target.CapitelizeText(text);
            return result;
            // TODO: add assertions to method CapitalizeServiceTest.CapitelizeTextTest(CapitalizeService, String)
        }
    }
}
