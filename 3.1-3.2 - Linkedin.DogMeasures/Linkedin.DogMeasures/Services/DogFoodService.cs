using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Linkedin.DogMeasures.Exceptions;
using Linkedin.DogMeasures.Models;

namespace Linkedin.DogMeasures.Services
{
	public class DogFoodService
	{

		private List<DogFood> _dogsFood;

		public DogFoodService()
		{
			_dogsFood = new List<DogFood> {
				new DogFood{ Breed = "afgano", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Akita", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "American Bully", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "American Foxhound", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "American Pitbull Terrier", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Beagle", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Bichón Frisé", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Bichón Maltés", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Bodeguero Andaluz", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Border Collie", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Border Terrier", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Bóxer", BallSize = DogFood.Size.Big, FoodWeight = 400, TimesPerDay = 1 },
				new DogFood{ Breed = "Bulldog", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Caniche", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Chihuahua", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Chow Chow", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Golden Retriever", BallSize = DogFood.Size.Small, FoodWeight = 150, TimesPerDay = 2 },
				new DogFood { Breed = "Labrador Retriever", BallSize = DogFood.Size.Medium, FoodWeight = 350, TimesPerDay = 2 },
			};
		}

		public DogFood GetByBreed(string breed)
		{
			if (breed == null)
			{
				throw new ArgumentNullException(nameof(breed));
			}
			var dogFood = _dogsFood.SingleOrDefault(d => d.Breed.Equals(breed, StringComparison.InvariantCultureIgnoreCase));
			if (dogFood == null)
			{
				throw new BreedNotFoundException($"No se encontró la raza {breed}.");
			}
			return dogFood;
		}


	}
}