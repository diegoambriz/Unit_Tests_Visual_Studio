using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaxCalculator.Contracts;
using TaxCalculator.Entities;

namespace TaxCalculator.Repositories
{
	public class RangeRepository : IRangeRepository
	{

		private readonly List<Range> _ranges;

		public RangeRepository()
		{
			string jsonFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ranges.json");
			_ranges = JsonConvert.DeserializeObject<List<Range>>(File.ReadAllText(jsonFile, Encoding.UTF8));
		}

		public Range GetRange(decimal grossSalary)
		{
			return _ranges.Single(r => grossSalary >= r.InclusiveMinValue && grossSalary <= (r.InclusiveMaxValue ?? grossSalary));
		}
	}
}