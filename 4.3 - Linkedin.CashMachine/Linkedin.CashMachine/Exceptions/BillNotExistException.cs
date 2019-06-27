using System;
using System.Collections.Generic;
using System.Text;

namespace Linkedin.CashMachine.Exceptions
{
	public class BillNotExistException : Exception
	{
		public BillNotExistException()
		{
		}

		public BillNotExistException(string message) : base(message)
		{
		}
	}
}
