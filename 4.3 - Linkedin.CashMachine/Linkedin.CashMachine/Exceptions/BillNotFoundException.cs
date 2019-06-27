using System;
using System.Collections.Generic;
using System.Text;

namespace Linkedin.CashMachine.Exceptions
{
	public class BillNotFoundException : Exception
	{
		public BillNotFoundException()
		{
		}

		public BillNotFoundException(string message) : base(message)
		{
		}
	}
}
