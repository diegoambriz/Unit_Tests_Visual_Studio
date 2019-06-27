using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkedin.DogMeasures.Models
{
	public class DogMeasures
	{

		public string Breed { get; set; }

		public decimal RecommendedMinWeight { get; set; }

		public decimal RecommendedMaxWeight { get; set; }

		public int LifeExpectancy { get; set; }



	}
}