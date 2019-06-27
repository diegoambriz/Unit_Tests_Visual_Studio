using System;
using System.Collections.Generic;
using System.Text;
using Linkedin.DogMeasures.Exceptions;
using Linkedin.DogMeasures.Repositories;
using Linkedin.DogMeasures.Services;
using Linkedin.DogMeasures.Tests.FakeClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linkedin.DogMeasures.Tests
{
[TestClass]
	public class DogMeasuresServiceShould
    {

		[TestMethod]
		[ExpectedException(typeof(BreedNotFoundException))]
		public void ThrowsBreedNotFoundException_IfBreedIsYorkshire()
		{
			var service = new DogMeasuresService(new FakeDogMeasuresRepository());
			var result = service.CheckDogIdealWeight("yorkshire", 14);
		}
    }
}
