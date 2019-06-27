using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Linkedin.DogMeasures.Models;

namespace Linkedin.DogMeasures.Contracts
{
	public interface IDogMeasuresService
	{

		DogWeightInfo CheckDogIdealWeight(string breed, decimal weight);

		int GetLifeExpectancy(string breed);

	}
}
