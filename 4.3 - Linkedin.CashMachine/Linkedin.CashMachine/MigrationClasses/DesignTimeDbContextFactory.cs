using Linkedin.CashMachine.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Linkedin.CashMachine
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CashMachineDbContext>
	{
		public CashMachineDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			var builder = new DbContextOptionsBuilder<CashMachineDbContext>();
			var connectionString = configuration.GetConnectionString("CashMachineConnection");
			builder.UseSqlite(connectionString);
			return new CashMachineDbContext(builder.Options);
		}
	}
}