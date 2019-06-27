using Linkedin.CashMachine.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.CashMachine.Contracts.Services
{
	public interface IBillService
	{

		Task<IEnumerable<int>> GetBillValuesAsync();

		Task<IEnumerable<Bill>> TakeMoneyAsync(int moneyAmmount);

		Task AddMoneyAsync(int billValue, int quantity);

		Task AddBillsAsync(IEnumerable<Bill> bills);

		Task<bool> CheckBillsAvailabilityAsync();

		Task<Bill> GetByValueAsync(uint value);

		Task<IEnumerable<Bill>> GetAvailableBillsAsync();

	}
}
