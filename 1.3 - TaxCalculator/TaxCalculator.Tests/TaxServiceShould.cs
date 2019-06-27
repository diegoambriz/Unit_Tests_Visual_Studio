using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstTestProject.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FirstTestProject.Tests
{
	[TestClass]
	public class TaxServiceShould
	{

		[TestMethod]
		public void Returns1000IfGrossSalaryIs9500()
		{
			var taxService = new TaxService();
			Assert.AreEqual(1000, taxService.GetTax(9500));
		}

[TestMethod]
		public void Returns2000IfGrossSalaryIs19500()
		{
			var taxService = new TaxService();
			Assert.AreEqual(2000, taxService.GetTax(19500));
		}

	}
}
