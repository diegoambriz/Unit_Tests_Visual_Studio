using System;
using System.Collections.Generic;
using System.Text;
using Linkedin.DogMeasures.Exceptions;
using Linkedin.DogMeasures.Services;
using Xunit;

namespace Linkedin.DogMeasures.Tests
{
	public class DogMeasuresServiceShould
	{

		private DogMeasuresService _dogMeasuresService;

		public DogMeasuresServiceShould()
		{
			_dogMeasuresService = new DogMeasuresService();
		}

		[Fact]
		public void ThrowsArgumentNullException_IfBreedIsNull()
		{
			Assert.Throws<ArgumentNullException>(() => _dogMeasuresService.CheckDogIdealWeight(null, 0));
		}

		[Fact]
		public void ThrowsBreedNotFoundException_IfBreedIsSamoyedo()
		{
			Assert.Throws<BreedNotFoundException>(() => _dogMeasuresService.CheckDogIdealWeight("samoyedo", 20));
		}

		[Fact]
		public void ThrowsArgumentOutOfRangeException_IfLengthIsNegative()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => _dogMeasuresService.CheckDogIdealWeight("bóxer", -5));
		}


		[InlineData(20)]
		[InlineData(21)]
		[InlineData(22)]
		[InlineData(23)]
		[InlineData(24)]
		[InlineData(25)]
		[InlineData(26)]
		[InlineData(27)]
		[InlineData(28)]
		[InlineData(29)]
		[InlineData(30)]
		[InlineData(31)]
		[InlineData(32)]
		[InlineData(33)]
		[InlineData(34)]
		[InlineData(35)]
		[Theory]
		public void DogIsInIdealWeight_IfBreedIsLabradorAndWeightInRange20And35(int weight)
		{
			var result = _dogMeasuresService.CheckDogIdealWeight("Labrador retriever", weight);
			Assert.True(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.InRange);
			Assert.Equal(0, result.WeightDeviation);
		}

		[InlineData(5)]
		[InlineData(6)]
		[InlineData(7)]
		[InlineData(8)]
		[InlineData(9)]
		[InlineData(10)]
		[InlineData(11)]
		[InlineData(12)]
		[InlineData(13)]
		[InlineData(14)]
		[InlineData(15)]
		[InlineData(16)]
		[InlineData(17)]
		[InlineData(18)]
		[InlineData(19)]
		[Theory]
		public void DogIsBelowWeight_IfBreedIsLabradorAndWeightInRange5And19(int weight)
		{
			var result = _dogMeasuresService.CheckDogIdealWeight("Labrador retriever", weight);
			Assert.True(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.BelowWeight);
			Assert.Equal(20 - weight, result.WeightDeviation);
		}


		[InlineData(36)]
		[InlineData(37)]
		[InlineData(38)]
		[InlineData(39)]
		[InlineData(40)]
		[Theory]
		public void DogIsInOverWeight_IfBreedIsLabradorAndWeightInRange36And40(int weight)
		{
			var result = _dogMeasuresService.CheckDogIdealWeight("Labrador retriever", weight);
			Assert.True(result.DeviationType == Models.DogWeightInfo.WeightDeviationType.Overweight);
			Assert.Equal(weight - 35, result.WeightDeviation);
		}

		[Fact]
		public void ReturnsLifeExpectancy14_IfBreedIsLabrador()
		{
			Assert.Equal(14, _dogMeasuresService.GetLifeExpectancy("Labrador retriever"));
		}





	}
}
