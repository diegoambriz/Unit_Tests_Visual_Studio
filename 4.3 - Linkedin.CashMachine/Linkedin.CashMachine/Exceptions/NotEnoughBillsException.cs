using System;
using System.Collections.Generic;
using System.Text;

namespace Linkedin.CashMachine.Exceptions
{
	public class NotEnoughBillsException : Exception
	{
		public NotEnoughBillsException()
		{
		}

		public NotEnoughBillsException(string message) : base(message)
		{
		}
	}
}
