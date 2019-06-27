using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTestProject.Services
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

		public string Capitalize(string text)
		{
			if (text.Length == 1)
			{
				return text.ToUpper();
			}
			if (text == String.Empty)
			{
				return String.Empty;
			}

			return text.Substring(0, 1).ToUpper() + text.Substring(1);
		}
	}
}
