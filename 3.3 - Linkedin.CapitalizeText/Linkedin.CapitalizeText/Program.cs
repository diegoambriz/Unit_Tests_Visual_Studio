using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkedin.CapitalizeText.Services;

namespace Linkedin.CapitalizeText
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Escribe algo");
			var text = Console.ReadLine();
			Console.WriteLine(new CapitalizeService().CapitelizeText(text));
			Console.ReadLine();


		}
	}
}
