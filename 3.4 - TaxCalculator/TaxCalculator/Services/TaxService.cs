using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Services
{
	public class TaxService
	{

		public decimal GetTax(decimal grossSalary)
		{
			if (grossSalary < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(grossSalary));
			}
			if (grossSalary < 8000)
			{
				return 1000;
			}
			if (grossSalary < 20000)
			{
				return 2000;
			}
			if (grossSalary < 40000)
			{
				return 3000;
			}
			if (grossSalary < 60000)
			{
				return 5000;
			}
			return 8000;
		}

		public int GetTaxesPercentageByCountry(string country)
		{
			switch (country.ToLower())
			{
				case "spain":
					return 21;
				case "france":
					return 18;
				case "austria":
					return 20;
				case "germany":
					return 19;
				default:
					return 0;
			}
		}

	}
}
