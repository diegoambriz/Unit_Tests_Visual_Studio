using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkedin.DogMeasures.Exceptions;
using Linkedin.DogMeasures.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linkedin.DogMeasures.Test.MSTest
{
	[TestClass]
	public class DogMeasuresServiceShould
	{

		private DogMeasuresService _dogMeasureService;

		[TestCategory("exception")]
		[TestMethod]
		[ExpectedException(typeof(BreedNotFoundException))]
		public void ThrowsBreedNotFoundException_IfBreedIsSamoyedo()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("samoyedo", 20);
		}

		[TestCategory("exception")]
		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void ThrowsArgumentNullException_IfBreedIsNull()
		{
			var result = _dogMeasureService.CheckDogIdealWeight(null, 5);
		}
[Priority(1)]

		[TestCategory("labrador")]
		[TestInitialize]
		public void TestSetup()
		{
			_dogMeasureService = new DogMeasuresService();
		}

		[Priority(1)]
		[TestCategory("labrador")]
		[TestMethod]
		public void DogIsInOverweight_IfBreedLabradorRetrieverAndWeight48()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("Labrador Retriever", 48);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.Overweight);
			Assert.AreEqual(13, result.WeightDeviation);
		}

		[Priority(1)]
		[TestCategory("labrador")]
		[TestMethod]
		public void DogIsInWeightRange_IfBreedLabradorRetrieverAndWeight33()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("Labrador Retriever", 33);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.InRange);
			Assert.AreEqual(0, result.WeightDeviation);
		}

[Priority(1)]		[TestCategory("labrador")]
		[TestMethod]
		public void DogIsBelowWeight_IfBreedLabradorRetrieverAndWeight17()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("Labrador Retriever", 17);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.BelowWeight);
			Assert.AreEqual(3, result.WeightDeviation);
		}

		[Priority(2)]
		[TestCategory("beagle")]
		[TestMethod]
		public void DogIsInOverweight_IfBreedBeagleAndWeight20()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("Beagle", 20);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.Overweight);
			Assert.AreEqual(6, result.WeightDeviation);
		}
[Owner("Juanjo")]
[TestProperty("BreedType", "Medium")]
		[Priority(2)]
		[TestCategory("beagle")]
		[TestMethod]
		public void DogIsInWeightRange_IfBreedBeagleAndWeight12()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("Beagle", 12);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.InRange);
			Assert.AreEqual(0, result.WeightDeviation);
		}

		[Priority(2)]
		[TestCategory("beagle")]
		[TestMethod]
		public void DogIsBelowWeight_IfBreedBeagleAndWeight5()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("Beagle", 5);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.BelowWeight);
			Assert.AreEqual(3, result.WeightDeviation);
		}

		[TestCategory("boxer")]
		[TestMethod]
		public void DogIsInOverweight_IfBreedBoxerAndWeight50()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("Bóxer", 50);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.Overweight);
			Assert.AreEqual(10, result.WeightDeviation);
		}

		[TestCategory("boxer")]
		[TestMethod]
		public void DogIsInWeightRange_IfBreedBoxerAndWeight31()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("Bóxer", 31);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.InRange);
			Assert.AreEqual(0, result.WeightDeviation);
		}

		[TestCategory("boxer")]
		[TestMethod]
		public void DogIsBelowWeight_IfBreedBoxerAndWeight16()
		{
			var result = _dogMeasureService.CheckDogIdealWeight("Bóxer", 16);
			Assert.IsTrue(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.BelowWeight);
			Assert.AreEqual(4, result.WeightDeviation);
		}
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ThrowsArgumentOutOfRangeException_IfWeightIsNegative()
		{
			var service = new DogMeasuresService();
			var result = service.CheckDogIdealWeight("labrador retriever", -5);
		}



	}
}
