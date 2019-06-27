using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linkedin.DogMeasures.Repositories;
using Linkedin.DogMeasures.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Linkedin.DogMeasures.tests
{
	[TestClass]
	public class DogMeasuresServiceShould
	{

		private Mock<DogMeasuresRepository> _mockRepository;

		[TestInitialize]
		public void Setup()
		{
			_mockRepository = new Mock<DogMeasuresRepository>();
			var dogsMeasures = new List<Models.DogMeasures> {
				new Models.DogMeasures { Breed = "afgano", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Akita", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "American Bully", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "American Foxhound", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "American Pitbull Terrier", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Beagle", RecommendedMinWeight = 8, RecommendedMaxWeight = 14, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Bichón Frisé", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Bichón Maltés", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Bodeguero Andaluz", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Border Collie", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Border Terrier", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Bóxer", RecommendedMinWeight = 20, RecommendedMaxWeight = 40, LifeExpectancy = 14 },
				new Models.DogMeasures{ Breed = "Bulldog", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Caniche", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Chihuahua", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Chow Chow", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Golden Retriever", RecommendedMinWeight = 20, RecommendedMaxWeight = 28, LifeExpectancy = 14 },
				new Models.DogMeasures { Breed = "Labrador Retriever", RecommendedMinWeight = 20, RecommendedMaxWeight = 35, LifeExpectancy = 14 }
			};

			_mockRepository.Setup(m => m.GetMeasures(It.IsAny<string>())).Returns<string>((breed) =>
			{
				return dogsMeasures.SingleOrDefault(m => m.Breed.Equals(breed, StringComparison.InvariantCultureIgnoreCase));
			});

		}

		[TestMethod]
		public void DogIsInOverweight_IfBreedIsBoxerAndWeightIs50()
		{
			var service = new DogMeasuresService(_mockRepository.Object);
			var result = service.CheckDogIdealWeight("bóxer", 50);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.Overweight);
		}

	}
}