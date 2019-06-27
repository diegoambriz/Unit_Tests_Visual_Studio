using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkedin.DogMeasures.Exceptions
{
	public class BreedNotFoundException : Exception
	{

		public BreedNotFoundException()
		{
		}

		public BreedNotFoundException(string message) : base(message)
		{
		}
	}
}