using Microsoft.Pex.Framework.Generated;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linkedin.CapitalizeText.Services;
// <copyright file="CapitalizeServiceTest.CapitelizeTextTest.g.cs">Copyright ©  2017</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace Linkedin.CapitalizeText.Services.Tests
{
	public partial class CapitalizeServiceTest
	{

[TestMethod]
[PexGeneratedBy(typeof(CapitalizeServiceTest))]
public void CapitelizeTextTest182()
{
    string s;
    CapitalizeService s0 = new CapitalizeService();
    s = this.CapitelizeTextTest(s0, "\0");
    Assert.AreEqual<string>("\0", s);
    Assert.IsNotNull((object)s0);
}

[TestMethod]
[PexGeneratedBy(typeof(CapitalizeServiceTest))]
public void CapitelizeTextTest514()
{
    string s;
    CapitalizeService s0 = new CapitalizeService();
    s = this.CapitelizeTextTest(s0, "h");
    Assert.AreEqual<string>("H", s);
    Assert.IsNotNull((object)s0);
}

[TestMethod]
[PexGeneratedBy(typeof(CapitalizeServiceTest))]
public void CapitelizeTextTest72()
{
    string s;
    CapitalizeService s0 = new CapitalizeService();
    s = this.CapitelizeTextTest(s0, "\b\0");
    Assert.AreEqual<string>("\b\0", s);
    Assert.IsNotNull((object)s0);
}

[TestMethod]
[PexGeneratedBy(typeof(CapitalizeServiceTest))]
[ExpectedException(typeof(ArgumentNullException))]
public void CapitelizeTextTestThrowsArgumentNullException317()
{
    string s;
    CapitalizeService s0 = new CapitalizeService();
    s = this.CapitelizeTextTest(s0, (string)null);
}

[TestMethod]
[PexGeneratedBy(typeof(CapitalizeServiceTest))]
public void CapitelizeTextTest410()
{
    string s;
    CapitalizeService s0 = new CapitalizeService();
    s = this.CapitelizeTextTest(s0, "");
    Assert.AreEqual<string>("", s);
    Assert.IsNotNull((object)s0);
}
	}
}
