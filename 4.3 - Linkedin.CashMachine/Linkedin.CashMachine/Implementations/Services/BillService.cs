using Linkedin.CashMachine.Contracts.Repositories;
using Linkedin.CashMachine.Contracts.Services;
using Linkedin.CashMachine.Entities;
using Linkedin.CashMachine.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.CashMachine.Implementations.Services
{
	public class BillService : IBillService
	{
		private IBillRepository _billRepository;

		public BillService(IBillRepository billRepository)
		{
			_billRepository = billRepository;
		}

		public async Task<IEnumerable<Bill>> TakeMoneyAsync(int moneyAmmount)
		{
			int remaining = moneyAmmount;

			var bills = (await _billRepository.GetAvailableBillsAsync()).OrderByDescending(b => b.Value);

			int totalAvailableInMachine = bills.Sum(b => b.Value * b.Quantity);
			if (totalAvailableInMachine == 0)
			{
				throw new CashMachineEmptyException("Este cajero no tiene efectivo. Lamentamos las molestias.");
			}
			if (totalAvailableInMachine < moneyAmmount)
			{
				throw new NotEnoughBillsException("El cajero no tiene suficientes billetes para satisfacer su petición de efectivo.");
			}

			var billsTaken = new Dictionary<int, int>();
			foreach (var bill in bills)
			{
				int numberOfBills = remaining / bill.Value;
				if (bill.Quantity < numberOfBills)
				{
					numberOfBills = bill.Quantity;
				}
				if (numberOfBills > 0)
				{
					billsTaken[bill.Value] = (billsTaken.ContainsKey(bill.Value) ? billsTaken[bill.Value] + numberOfBills : numberOfBills);
					remaining -= bill.Value * numberOfBills;
				}
				if (remaining == 0)
				{
					break;
				}
			}
			if (remaining > 0)
			{
				if (bills.Any(b => b.Value == remaining))
				{
					throw new BillNotFoundException($"El cajero no dispone de billetes de {moneyAmmount}€.");
				}
				else
				{
					throw new BillNotExistException($"No puede sacar {moneyAmmount}€, ya que no existen billetes de {remaining}€. Por favor, elija una cantidad que podamos satisfacer con billetes.");
				}
			}
			foreach (var billTaken in billsTaken)
			{
				var billFromDb = await _billRepository.GetByValueAsync(billTaken.Key);
				billFromDb.Quantity -= billTaken.Value;
			}
			await _billRepository.SaveChangesAsync();
			return billsTaken.Select(item => new Bill { Value = item.Key, Quantity = item.Value });
		}

		public async Task<IEnumerable<int>> GetBillValuesAsync()
		{
			return (await _billRepository.GetAvailableBillsAsync()).Select(b => b.Value);
		}

		public async Task<bool> CheckBillsAvailabilityAsync()
		{
			int totalAvailableInMachine = (await _billRepository.GetAvailableBillsAsync()).Sum(b => b.Value * b.Quantity);
			if (totalAvailableInMachine == 0)
			{
				return false;
			}
			return true;
		}

		public async Task AddMoneyAsync(int billValue, int quantity)
		{
			var billFromDb = await _billRepository.GetByValueAsync(billValue);
			if (billFromDb == null)
			{
				var newBill = new Bill { Value = billValue, Quantity = quantity };
				_billRepository.Add(newBill);
			}
			else
			{
				billFromDb.Quantity += quantity;
			}
			_billRepository.SaveChanges();
		}

		public async Task AddBillsAsync(IEnumerable<Bill> bills)
		{
			_billRepository.AddRange(bills);
			await _billRepository.SaveChangesAsync();
		}

		public Task<Bill> GetByValueAsync(uint value)
		{
			return _billRepository.GetByValueAsync((int)value);
		}

		public Task<IEnumerable<Bill>> GetAvailableBillsAsync()
		{
			return _billRepository.GetAvailableBillsAsync();
		}
	}
}
