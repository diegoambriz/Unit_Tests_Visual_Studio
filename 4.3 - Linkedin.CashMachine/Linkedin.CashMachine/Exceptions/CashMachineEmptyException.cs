using System;
using System.Collections.Generic;
using System.Text;

namespace Linkedin.CashMachine.Exceptions
{
	public class CashMachineEmptyException : Exception
	{
		public CashMachineEmptyException()
		{
		}

		public CashMachineEmptyException(string message) : base(message)
		{
		}
	}
}
