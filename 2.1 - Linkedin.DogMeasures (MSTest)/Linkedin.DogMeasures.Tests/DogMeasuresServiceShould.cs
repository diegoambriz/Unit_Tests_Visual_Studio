using System;
using System.Collections.Generic;
using System.Text;
using Linkedin.DogMeasures.Exceptions;
using Linkedin.DogMeasures.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linkedin.DogMeasures.Tests
{
[TestClass]
	public class DogMeasuresServiceShould
	{

		private static DogMeasuresService _dogMeasuresService;

		[ClassInitialize]
		public static void Setup(TestContext context)
		{
			_dogMeasuresService = new DogMeasuresService();
		}

		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void ThrowsArgumentNullException_IfBreedIsNull()
		{
			var result = _dogMeasuresService.CheckDogIdealWeight(null, 0);
		}

		[TestMethod]
		[ExpectedException(typeof(BreedNotFoundException))]
		public void ThrowsBreedNotFoundException_IfBreedIsSamoyedo()
		{
			var result = _dogMeasuresService.CheckDogIdealWeight("samoyedo", 20);
		}


		[DataTestMethod]
		[DataRow("Labrador Retriever", 20)]
		[DataRow("Labrador Retriever", 21)]
		[DataRow("Labrador Retriever", 22)]
		[DataRow("Labrador Retriever", 23)]
		[DataRow("Labrador Retriever", 24)]
		[DataRow("Labrador Retriever", 25)]
		[DataRow("Labrador Retriever", 26)]
		[DataRow("Labrador Retriever", 27)]
		[DataRow("Labrador Retriever", 28)]
		[DataRow("Labrador Retriever", 29)]
		[DataRow("Labrador Retriever", 30)]
		[DataRow("Labrador Retriever", 31)]
		[DataRow("Labrador Retriever", 32)]
		[DataRow("Labrador Retriever", 33)]
		[DataRow("Labrador Retriever", 34)]
		[DataRow("Labrador Retriever", 35)]
		public void DogIsInIdealWeight_IfBreedIsLabradorAndWeightInRange20And40(string breed, int weight)
		{
			var result = _dogMeasuresService.CheckDogIdealWeight(breed, weight);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.InRange);
			Assert.AreEqual(0, result.WeightDeviation);
		}

		[DataTestMethod]
		[DataRow("Labrador Retriever", 5)]
		[DataRow("Labrador Retriever", 6)]
		[DataRow("Labrador Retriever", 7)]
		[DataRow("Labrador Retriever", 8)]
		[DataRow("Labrador Retriever", 9)]
		[DataRow("Labrador Retriever", 10)]
		[DataRow("Labrador Retriever", 11)]
		[DataRow("Labrador Retriever", 12)]
		[DataRow("Labrador Retriever", 13)]
		[DataRow("Labrador Retriever", 14)]
		[DataRow("Labrador Retriever", 15)]
		[DataRow("Labrador Retriever", 16)]
		[DataRow("Labrador Retriever", 17)]
		[DataRow("Labrador Retriever", 18)]
		[DataRow("Labrador Retriever", 19)]
		public void DogIsBelowWeight_IfBreedIsLabradorAndWeightInRange5And19(string breed, int weight)
		{
			var result = _dogMeasuresService.CheckDogIdealWeight(breed, weight);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.BelowWeight);
			Assert.AreEqual(20 - weight, result.WeightDeviation);
		}


		[DataTestMethod]
		[DataRow("Labrador Retriever", 36)]
		[DataRow("Labrador Retriever", 37)]
		[DataRow("Labrador Retriever", 38)]
		[DataRow("Labrador Retriever", 39)]
		[DataRow("Labrador Retriever", 40)]
		public void DogIsInOverWeight_IfBreedIsLabradorAndWeightInRange36And40(string breed, int weight)
		{
			var result = _dogMeasuresService.CheckDogIdealWeight(breed, weight);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.Overweight);
			Assert.AreEqual(weight - 35, result.WeightDeviation);
		}

		[TestMethod]
		public void ReturnsLifeExpectancy14_IfBreedIsLabrador()
		{
			Assert.AreEqual(14, _dogMeasuresService.GetLifeExpectancy("Labrador retriever"));
		}



	}
}
