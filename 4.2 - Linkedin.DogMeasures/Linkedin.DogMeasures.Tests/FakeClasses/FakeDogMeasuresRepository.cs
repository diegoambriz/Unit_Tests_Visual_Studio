using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linkedin.DogMeasures.Contracts;
using Linkedin.DogMeasures.Models;

namespace Linkedin.DogMeasures.Tests.FakeClasses
{
	public class FakeDogMeasuresRepository : IDogMeasuresRepository
	{
		private readonly List<Models.DogMeasures> _dogsMeasures;

		public FakeDogMeasuresRepository()
		{
			_dogsMeasures = new List<Models.DogMeasures> {
				new Models.DogMeasures { Breed = "afgano", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Akita", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "American Bully", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "American Foxhound", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "American Pitbull Terrier", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Beagle", RecommendedMinWeight = 8, RecommendedMaxWeight = 14, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Bichón Frisé", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Bichón Maltés", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Bodeguero Andaluz", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 }
			};
		}



		public Models.DogMeasures GetMeasures(string breed)
		{
			var dog = _dogsMeasures.SingleOrDefault(d => d.Breed.Equals(breed, StringComparison.InvariantCultureIgnoreCase));
			return dog;
		}
	}
}
