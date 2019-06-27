using System;
using System.Collections.Generic;
using System.Text;

namespace Linkedin.CashMachine.Contracts.Services
{
	interface IPinValidationService
	{

		bool ValidatePin(string pin);

	}
}
