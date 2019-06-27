using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.CapitalizeText.Services
{
	public class CapitalizeService
	{

		public string CapitelizeText(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException(nameof(text));
			}


			if (text.Length == 0)
			{
				return String.Empty;
			}

			if (text.Length == 1)
			{
				return text.ToUpper();
			}
			return text.Substring(0, 1).ToUpper() + text.Substring(1);
		}

	}
}
