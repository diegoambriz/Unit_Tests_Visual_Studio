using Linkedin.CashMachine.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Linkedin.CashMachine.Entities;
using Linkedin.CashMachine.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Linkedin.CashMachine.Implementations.Repositories
{
	public class BillRepository : IBillRepository
	{

		private readonly CashMachineDbContext _context;

		public BillRepository(CashMachineDbContext context)
		{
			_context = context;
		}

		public void Add(Bill b)
		{
			_context.Bills.Add(b);
		}

		public void AddRange(IEnumerable<Bill> bills)
		{
			_context.Bills.AddRange(bills);
		}

		/// <summary>
		/// Gets the available bills in the cash machine.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<IEnumerable<Bill>> GetAvailableBillsAsync()
		{
			return await _context.Bills.ToListAsync();

		}

		public Task<Bill> GetByValueAsync(int value)
		{
			return _context.Bills.SingleOrDefaultAsync(b => b.Value == value);
		}

		public int SaveChanges()
		{
			return _context.SaveChanges();
		}

		public Task<int> SaveChangesAsync()
		{
			return _context.SaveChangesAsync();
		}
	}
}
