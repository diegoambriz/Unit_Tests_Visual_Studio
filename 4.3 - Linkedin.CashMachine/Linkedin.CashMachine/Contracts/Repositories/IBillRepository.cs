using Linkedin.CashMachine.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.CashMachine.Contracts.Repositories
{
	public interface IBillRepository
	{

		void Add(Bill b);

		void AddRange(IEnumerable<Bill> bills);

		Task<Bill> GetByValueAsync(int value);

		Task<IEnumerable<Bill>> GetAvailableBillsAsync();

		int SaveChanges();

		Task<int> SaveChangesAsync();

	}
}
