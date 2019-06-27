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
	public class DogFoodServiceShould
	{

		private DogFoodService _dogFoodService;

		[TestInitialize]
		public void TestSetup()
		{
			_dogFoodService = new DogFoodService();
		}

		[TestCategory("exception")]
		[TestMethod]
		[ExpectedException(typeof(BreedNotFoundException))]
		public void ThrowsBreedNotFoundException_IfBreedIsSamoyedo()
		{
			var result = _dogFoodService.GetByBreed("samoyedo");
		}

		[TestMethod]
		[TestCategory("exception")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ThrowsArgumentNullException_IfBreedIsNull()
		{
			var result = _dogFoodService.GetByBreed(null);
		}


		[TestCategory("labrador")]
		[TestMethod]
		public void ReturnsBallSizeMedium_AndTimesPerDay2_AndFoodWeight350_IfBreedIsLabradorRetriever()
		{
			var result = _dogFoodService.GetByBreed("Labrador Retriever");
			Assert.IsTrue(result.BallSize == Models.DogFood.Size.Medium);
			Assert.AreEqual(2, result.TimesPerDay);
			Assert.AreEqual(350, result.FoodWeight);
		}

		[TestCategory("beagle")]
		[TestMethod]
		public void ReturnsBallSizeSmall_AndTimesPerDay2_AndFoodWeight150_IfBreedIsLabradorRetriever()
		{
			var result = _dogFoodService.GetByBreed("Beagle");
			Assert.IsTrue(result.BallSize == Models.DogFood.Size.Small);
			Assert.AreEqual(2, result.TimesPerDay);
			Assert.AreEqual(150, result.FoodWeight);
		}

		[TestCategory("boxer")]
		[TestMethod]
		public void ReturnsBallSizeBig_AndTimesPerDay1_AndFoodWeight400_IfBreedIsBoxer()
		{
			var result = _dogFoodService.GetByBreed("Bóxer");
			Assert.IsTrue(result.BallSize == Models.DogFood.Size.Big);
			Assert.AreEqual(1, result.TimesPerDay);
			Assert.AreEqual(400, result.FoodWeight);
		}


	}
}
