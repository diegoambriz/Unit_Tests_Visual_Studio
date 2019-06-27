using Linkedin.CashMachine.Config;
using Linkedin.CashMachine.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Linkedin.CashMachine.Implementations.Services
{
	public class PinValidationService : IPinValidationService
	{
		private readonly PinConfig _config;

		public PinValidationService(PinConfig config)
		{
			_config = config;
		}

		public bool ValidatePin(string pin)
		{
			return _config.Pin.Equals(pin, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
