using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Repositories;

namespace TaxCalculator.Tests
{
	[TestClass]
	public class TaxServiceShould
	{

		[TestMethod]
		public void Returns1805IfGrossSalaryIs9500()
		{
			var taxService = new TaxService(new RangeRepository());
			Assert.AreEqual(1805, taxService.GetTaxes(9500));
		}

		[TestMethod]
		public void Returns4680IfGrossSalaryIs19500()
		{
			var taxService = new TaxService(new RangeRepository());
			Assert.AreEqual(4680, taxService.GetTaxes(19500));
		}

	}
}
