using System;
using System.Collections.Generic;
using System.Text;
using Linkedin.DogMeasures.Exceptions;
using Linkedin.DogMeasures.Services;
using NUnit.Framework;

namespace Linkedin.DogMeasures.Tests.NUnit
{
	[TestFixture]
	public class DogMeasuresServiceShould
	{
		private DogMeasuresService _dogMeasuresService;

		[SetUp]
		public void Setup()
		{
			_dogMeasuresService = new DogMeasuresService();
		}
		[Test]
		public void ThrowsBreedNotFoundException_IfBreedIsSamoyedo()
		{
			Assert.Throws<BreedNotFoundException>(() => _dogMeasuresService.CheckDogIdealWeight("samoyedo", 20));
		}

		[Test]
		public void ThrowsArgumentNullException_IfBreedIsNull()
		{
			Assert.Throws<ArgumentNullException>(() => _dogMeasuresService.CheckDogIdealWeight(null, 5));
		}


		[Test]
		public void DogIsInIdealWeight_IfBreedIsLabradorAndWeightInRange20And35([Range(20, 35)]int weight)
		{
			var result = _dogMeasuresService.CheckDogIdealWeight("Labrador retriever", weight);
			Assert.True(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.InRange);
			Assert.AreEqual(0, result.WeightDeviation);
		}

		[Test]
		public void DogIsBelowWeight_IfBreedIsLabradorAndWeightInRange5And19([Range(5, 19)]int weight)
		{
			var result = _dogMeasuresService.CheckDogIdealWeight("Labrador retriever", weight);
			Assert.True(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.BelowWeight);
			Assert.AreEqual(20 - weight, result.WeightDeviation);
		}


		[Test]
		public void DogIsInOverWeight_IfBreedIsLabradorAndWeightInRange36And40([Range(36, 40)]int weight)
		{
			var result = _dogMeasuresService.CheckDogIdealWeight("Labrador retriever", weight);
			Assert.True(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.Overweight);
			Assert.AreEqual(weight - 35, result.WeightDeviation);
		}

		[Test]
		public void ReturnsLifeExpectancy14_IfBreedIsLabrador()
		{
			Assert.AreEqual(14, _dogMeasuresService.GetLifeExpectancy("Labrador retriever"));
		}



	}
}
