using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkedin.DogMeasures.Models
{
	public class DogWeightInfo
	{
		public enum WeightDeviationType
		{
			InRange,
			BelowWeight,
			Overweight
		}


		public WeightDeviationType DeviationType { get; set; }

		public string Message { get; set; }

		public decimal WeightDeviation { get; set; }

	}
}