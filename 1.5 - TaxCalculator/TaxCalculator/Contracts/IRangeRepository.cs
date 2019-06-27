using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Entities;

namespace TaxCalculator.Contracts
{
	public interface IRangeRepository
	{

		Range GetRange(decimal grossSalary);
	}
}
