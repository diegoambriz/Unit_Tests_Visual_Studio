using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Linkedin.DogMeasures.Exceptions;
using Linkedin.DogMeasures.Models;

namespace Linkedin.DogMeasures.Services
{
	public class DogMeasuresService
	{

		private readonly List<Models.DogMeasures> _dogInfo;

		public DogMeasuresService()
		{
			_dogInfo = new List<Models.DogMeasures> {
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
		}

		public DogWeightInfo CheckDogIdealWeight(string breed, decimal weight)
		{
			if (breed == null)
			{
				throw new ArgumentNullException(nameof(breed));
			}
			if (weight <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(weight));
			}
			var dog = _dogInfo.SingleOrDefault(d => d.Breed.Equals(breed, StringComparison.InvariantCultureIgnoreCase));
			if (dog == null)
			{
				throw new BreedNotFoundException("No se encontró esa raza.");
			}
			if (weight > dog.RecommendedMaxWeight)
			{
				var deviation = weight - dog.RecommendedMaxWeight;
				return new DogWeightInfo { DeviationType = DogWeightInfo.WeightDeviationType.Overweight, Message = $"Tu perro se pasa del peso ideal por {deviation} KG.", WeightDeviation = deviation };
			}
			else if (weight < dog.RecommendedMinWeight)
			{
				var deviation = dog.RecommendedMinWeight - weight;
				return new DogWeightInfo
				{
					DeviationType = DogWeightInfo.WeightDeviationType.BelowWeight,
					Message = $"Creo que deberías darle un poco más de comer  a tu perro! " +
					$"Debería engordar {deviation} KG para empezar a estar en su peso ideal.",
					WeightDeviation = deviation
				};
			}
			return new DogWeightInfo { Message = "¡Tu perro está en su peso ideal! ¡Enhorabuena!", DeviationType = DogWeightInfo.WeightDeviationType.InRange, WeightDeviation = 0m };
		}

		public int GetLifeExpectancy(string breed)
		{
			var dog = _dogInfo.SingleOrDefault(d => d.Breed.Equals(breed, StringComparison.InvariantCultureIgnoreCase));
			if (dog == null)
			{
				throw new BreedNotFoundException("No se encontró esa raza.");
			}
			return dog.LifeExpectancy;
		}


	}
}