using Linkedin.CashMachine.Config;
using Linkedin.CashMachine.Context;
using Linkedin.CashMachine.Contracts.Repositories;
using Linkedin.CashMachine.Contracts.Services;
using Linkedin.CashMachine.Entities;
using Linkedin.CashMachine.Implementations.Repositories;
using Linkedin.CashMachine.Implementations.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Linkedin.CashMachine
{
	class Program
	{
		static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder();
			builder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			builder.AddJsonFile("appsettings.json");
			var configuration = builder.Build();
			var serviceProvider = BuildServiceProvider(configuration);
			Seed(serviceProvider.GetService<IBillService>());
			UseCashMachine(serviceProvider.GetService<IBillService>(), serviceProvider.GetService<IPinValidationService>());
		}

		private static ServiceProvider BuildServiceProvider(IConfigurationRoot configuration)
		{
			var serviceProvider = new ServiceCollection()
							.AddSingleton<IBillRepository, BillRepository>()
							.AddSingleton<IBillService, BillService>()
							.AddEntityFrameworkSqlite()
							.AddDbContext<CashMachineDbContext>(options => options.UseSqlite(configuration.GetConnectionString("cashMachineConnection")))
							.AddSingleton<PinConfig>(ct => new PinConfig { Pin = "1234" }) /* TODO: Change for DI */
							.AddSingleton<IPinValidationService, PinValidationService>()
							.BuildServiceProvider();
			return serviceProvider;
		}

		private static void DepositMoney(IBillService billService)
		{
			Console.WriteLine("Introduce a continuación los billetes que vas a ingresar. Para finalizar, pulsa enter sin escribir el valor de un billete.");
			do
			{
				Console.WriteLine("Valor del billete:");
				var valueStr = Console.ReadLine();
				if (String.IsNullOrWhiteSpace(valueStr))
				{
					Console.WriteLine("Saliendo...");
					return;
				}
				if (!uint.TryParse(valueStr, out uint value))
				{
					Console.WriteLine($"{valueStr} no es un valor entero positivo válido como valor de billete.");
					continue;
				}
				Bill bill = billService.GetByValueAsync(value).Result;
				if (bill == null)
				{
					Console.WriteLine($"Este cajero no admite billetes de {value} EUR.");
					continue;
				}
				Console.WriteLine($"¿Cuántos billetes de {value} EUR vas a ingresar?");
				var quantityStr = Console.ReadLine();
				if (!uint.TryParse(quantityStr, out uint quantity))
				{
					Console.WriteLine("La cantidad no es un valor positivo válido.");
					continue;
				}
				billService.AddMoneyAsync((int)value, (int)quantity).Wait();
				Console.WriteLine($"Has ingresado {quantity} billetes de {value} EUR.");
			} while (true);
		}

		private static void UseCashMachine(IBillService billService, IPinValidationService pinValidationService)
		{
			Console.WriteLine("Por favor, introduzca su PIN.");
			string pin = Console.ReadLine();
			if (!Regex.IsMatch(pin, @"^\d{4}$"))
			{
				Console.WriteLine("El PIN debe contener cuatro números.");
				return;
			}
			if (!pinValidationService.ValidatePin(pin))
			{
				Console.WriteLine("PIN no válido.");
				return;
			}
			string choiceStr = null;
			do
			{
				Console.WriteLine("1. Sacar dinero." + Environment.NewLine +
					"2. Ingresar dinero." + Environment.NewLine +
					"3. Ver el efectivo que tiene el cajero (solo para superadministradores del cajero ;) )" + Environment.NewLine +
					"4. Salir.");
				choiceStr = Console.ReadLine();
				if (!Int32.TryParse(choiceStr, out int choice))
				{
					Console.WriteLine($"{choiceStr} no es un valor de menú válido.");
					continue;
				}
				switch (choice)
				{
					case 1:
						ExtractMoney(billService);
						break;
					case 2:
						DepositMoney(billService);
						break;
					case 3:
						ViewAvailableCash(billService);
						break;
					case 4:
						Console.WriteLine("Recoja su tarjeta ;) Pulse Enter para salir.");
						Console.ReadLine();
						return;
					default:
						Console.WriteLine($"{choice} no es un ítem válido en el menú.");
						break;
				}
			} while (true);
		}

		private static void ViewAvailableCash(IBillService billService)
		{
			if (!billService.CheckBillsAvailabilityAsync().Result)
			{
				Console.WriteLine("¡El cajero está pelado! :)");
				return;
			}
			var bills = billService.GetAvailableBillsAsync().Result;
			var billsStr = bills.Where(b => b.Quantity > 0).OrderBy(b => b.Value).Select(b => $"{b.Quantity} billetes de {b.Value} EUR");
			Console.WriteLine($"Este cajero dispone del siguiente efectivo: " + Environment.NewLine +
				ConcatenateWithConjunction(billsStr, "y") + ".");
		}

		private static void ExtractMoney(IBillService billService)
		{
			if (!billService.CheckBillsAvailabilityAsync().Result)
			{
				Console.WriteLine("¡Lo sentimos! Este cajero no tiene efectivo en este momento!");
				Console.WriteLine("Pulsa Enter para continuar.");
				Console.ReadLine();

				return;

			}
			Console.WriteLine("¿Qué cantidad quieres sacar? Pulsa enter para cancelar la operación");
			var bills = billService.GetBillValuesAsync().Result.OrderBy(b => b).Select(b => b.ToString() + " eur");
			var existingBillsStr = ConcatenateWithConjunction(bills, "y");
			Console.WriteLine($"Hay billetes de {existingBillsStr}.");
			uint money = 0;

			do
			{
				var line = Console.ReadLine();
				if (String.IsNullOrWhiteSpace(line))
				{
					Console.WriteLine("Saliendo...");
					return;
				}
				if (!uint.TryParse(line, out money))
				{
					Console.WriteLine("Esa cantidad no es válida. Debes utilizar un número entero positivo.");
				}
			} while (money == 0);
			Console.WriteLine($"Vas a sacar {money} eur. Pulsa enter para continuar, cualquier otra tecla para cancelar.");
			var key = Console.ReadKey();
			if (key.Key == ConsoleKey.Enter)
			{
				try
				{
					var billsExtracted = billService.TakeMoneyAsync((int)money).Result;
					var orderedBills = billsExtracted.OrderByDescending(b => b.Value).Select(b => b.Quantity + " billete de " + b.Value + " EUR");
					string billsStr = ConcatenateWithConjunction(orderedBills, "y");
					Console.WriteLine("Aquí tienes " + billsStr);
				}
				catch (AggregateException ex)
				{
					Console.WriteLine(GetAggregateExceptionMessages(ex));
				}
				catch (Exception ex)
				{
					Console.WriteLine("Se ha producido la siguiente incidencia:" + Environment.NewLine +
						ex.Message);
				}
			}
			else
			{
				Console.WriteLine("Cancelando.");
			}
		}

		private static string GetAggregateExceptionMessages(AggregateException ex)
		{
			StringBuilder message = new StringBuilder();
			if (ex.InnerExceptions.Count > 1)
			{
				message.AppendLine("Se han producido las siguientes incidencias:");
			}
			else
			{
				message.AppendLine("Se ha producido la siguiente incidencia:");
			}
			message.AppendLine(String.Join(Environment.NewLine, ex.InnerExceptions.Select(inner => inner.Message).ToArray()));
			return message.ToString();
		}

		private static string ConcatenateWithConjunction(IEnumerable<string> items, string conjunction)
		{
			if (items == null)
			{
				throw new ArgumentNullException(nameof(items));
			}
			if (!items.Any())
			{
				return String.Empty;
			}
			var str = String.Join(", ", items.Take(items.Count() - 1).ToArray());
			str += (str.Length > 0 ? $" {conjunction} " : "") + items.Last();
			return str;
		}

		private static void Seed(IBillService billService)
		{
			if (!billService.GetBillValuesAsync().Result.Any())
			{
				billService.AddBillsAsync(new List<Bill> {
				new Bill { Value = 5, Quantity = 20 },
				new Bill { Value = 10, Quantity = 60 },
				new Bill { Value = 20, Quantity = 80 },
				new Bill { Value = 50, Quantity = 100 }
			});
			}
		}
	}


	}
