using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaxCalculator.Tests
{
	[TestClass]
	public class TaxServiceTests
	{
		private static TaxService _taxService;

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			_taxService = new TaxService();
		}

		[TestMethod]
		public void GetTaxReturns1000_IfGrossSalaryIs7500()
		{
			var result = _taxService.GetTax(7500);
			Assert.AreEqual(1000, result);
		}

		[TestMethod]
		public void GetTaxReturns2000_IfGrossSalaryIs1000()
		{
			var result = _taxService.GetTax(12345);
			Assert.AreEqual(2000, result);
		}

		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void GetTaxThrowsArgumentOutOfRangeException_IfGrossSalaryIsNegative()
		{
			var result = _taxService.GetTax(-3);
		}


		[TestMethod]
		public void GetTaxesPercentageByCountryReturns21_IfCountryIsSpain()
		{
			var percentage = _taxService.GetTaxesPercentageByCountry("spain");
			Assert.AreEqual(21, percentage);
		}


	}
}
