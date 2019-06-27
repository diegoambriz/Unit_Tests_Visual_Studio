using Linkedin.CashMachine.Entities;
using Microsoft.EntityFrameworkCore;

namespace Linkedin.CashMachine.Context
{
	public class CashMachineDbContext : DbContext
	{

		public DbSet<Bill> Bills { get; set; }


		public CashMachineDbContext(DbContextOptions options) : base(options)
		{
		}

		public CashMachineDbContext()
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var b = modelBuilder.Entity<Bill>();
			b.HasKey(p => p.Value);
			base.OnModelCreating(modelBuilder);
		}
	}
}
