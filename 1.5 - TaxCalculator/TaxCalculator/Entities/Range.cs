using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Entities
{
	public class Range
	{

		public decimal InclusiveMinValue { get; set; }

		public decimal? InclusiveMaxValue { get; set; }

		public int Percentage { get; set; }

	}
}
