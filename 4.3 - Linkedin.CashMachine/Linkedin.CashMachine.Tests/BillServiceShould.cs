using Linkedin.CashMachine.Contracts.Repositories;
using Linkedin.CashMachine.Contracts.Services;
using Linkedin.CashMachine.Entities;
using Linkedin.CashMachine.Exceptions;
using Linkedin.CashMachine.Implementations.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linkedin.CashMachine.Tests
{
	[TestClass]
	public class BillServiceShould
	{

		private Mock<IBillRepository> ConfigureMockWithBills(List<Bill> bills)
		{
			var mockRepository = new Mock<IBillRepository>();
			mockRepository.Setup(p => p.GetAvailableBillsAsync()).Returns(
					Task.FromResult(bills.AsEnumerable()));
			mockRepository.Setup(m => m.GetByValueAsync(It.IsAny<int>()))
				.Returns<int>((value) => Task.FromResult(bills.SingleOrDefault(item => item.Value == value)))
				.Verifiable();

			mockRepository.Setup(m => m.Add(It.IsAny<Bill>()))
					.Callback<Bill>((b) =>
					{
						bills.Add(b);
					})
					.Verifiable();

			return mockRepository;
		}

		private IBillService CreateBillServiceWithBills(List<Bill> bills)
		{
			return new BillService(ConfigureMockWithBills(bills).Object);
		}

		[TestInitialize]
		public void TestSetup()
		{
		}

		[TestMethod]
		public async Task MachineHasNotCashIfAllBillSlotsHaveZeroBills()
		{
			var billService = CreateBillServiceWithBills(
				new List<Bill>
				{
					new Bill { Value = 5, Quantity = 0 },
					new Bill { Value = 10, Quantity = 0 },
					new Bill { Value = 20, Quantity = 0 },
					new Bill { Value = 50, Quantity = 0 }
				});
			Assert.IsFalse(await billService.CheckBillsAvailabilityAsync());
		}

		[TestMethod]
		public async Task MachineHasCashIfAnySlotHasBills()
		{
			var billService = CreateBillServiceWithBills(
				new List<Bill>
				{
					new Bill { Value = 5, Quantity = 0 },
					new Bill { Value = 10, Quantity = 0 },
					new Bill { Value = 20, Quantity = 1 },
					new Bill { Value = 50, Quantity = 0 }
				});
			Assert.IsTrue(await billService.CheckBillsAvailabilityAsync());
		}

		[TestMethod]
		public async Task MachineHasOne5EurBill_IfItsSlotIsEmptyAndAddOne5Eur()
		{
			var billList = new List<Bill> {
				new Bill { Value = 5, Quantity = 0 }
			};
			var mockRepository = ConfigureMockWithBills(billList);
			var billService = new BillService(mockRepository.Object);
			await billService.AddMoneyAsync(5, 1);
			Assert.AreEqual(1, billList.Count);
			Assert.AreEqual(1, billList.First().Quantity);
			mockRepository.Verify(p => p.Add(It.IsAny<Bill>()), Times.Never);
		}

		[TestMethod]
		public async Task Gets5EurBillAnd10EurBill_IfTake15EurAndThereAreThree5EurBillAndOne10EurBill()
		{
			var mockRepository = ConfigureMockWithBills(new List<Bill> {
				new Bill { Value = 5, Quantity = 3 },
				new Bill { Value = 10, Quantity = 1 }
			});

			var billService = new BillService(mockRepository.Object);
			var billsTaken = await billService.TakeMoneyAsync(15);
			Assert.AreEqual(2, billsTaken.Count());
			Assert.IsTrue(billsTaken.Any(b => b.Value == 5 && b.Quantity == 1));
			Assert.IsTrue(billsTaken.Any(b => b.Value == 10 && b.Quantity == 1));
		}

		[TestMethod]
		[ExpectedException(typeof(BillNotExistException))]
		public async Task ThrowsBillNotExistsException_IfTake2Eur()
		{
			var billService = CreateBillServiceWithBills(new List<Bill> { new Bill { Value = 5, Quantity = 10 } });
			var result = await billService.TakeMoneyAsync(2);
		}

		[ExpectedException(typeof(CashMachineEmptyException))]
		[TestMethod]
		public async Task ThrowsCashMachineEmptyException_IfTake5EurAndThereAreNoBills()
		{
			var billService = CreateBillServiceWithBills(new List<Bill> {
				new Bill { Value = 5, Quantity = 0 },
				new Bill { Value = 10, Quantity = 0 },
				new Bill { Value = 20, Quantity = 0 },
				new Bill { Value = 50, Quantity = 0 }
			});
			var result = await billService.TakeMoneyAsync(5);
		}

		[ExpectedException(typeof(BillNotFoundException))]
		[TestMethod]
		public async Task ThrowsBillNotFoundException_IfTake5EurAndThereAreNo5EurBills()
		{
			var billService = CreateBillServiceWithBills(new List<Bill> {
				new Bill { Value = 5, Quantity = 0 },
				new Bill { Value = 20, Quantity = 1 }
			});
			var result = await billService.TakeMoneyAsync(5);
		}

		[ExpectedException(typeof(NotEnoughBillsException))]
		[TestMethod]
		public async Task ThrowsNotEnoughBillsException_IfTake200EurAndThereAreONly3BillsOf50Eur()
		{
			var billService = CreateBillServiceWithBills(new List<Bill> {
				new Bill { Value = 50, Quantity = 3 },
			});
			var result = await billService.TakeMoneyAsync(200);
		}



	}
}
