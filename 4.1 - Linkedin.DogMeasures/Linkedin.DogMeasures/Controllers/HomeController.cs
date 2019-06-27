using System;
using System.Threading.Tasks;
using Linkedin.DogMeasures.Contracts;
using Linkedin.DogMeasures.Models;
using Linkedin.DogMeasures.Services;
using Microsoft.AspNetCore.Mvc;

namespace Linkedin.DogMeasures.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDogMeasuresService _dogMeasuresService;

		public HomeController(IDogMeasuresService service)
		{
			_dogMeasuresService = service;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index([FromForm]DogInfoRequest info)
		{
			if (!ModelState.IsValid)
			{
				return View(info);
			}
			try
			{
				var measures = _dogMeasuresService.CheckDogIdealWeight(info.Breed, info.Weight);
				return await Task.FromResult(View("MeasuresResults", measures));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", $"Error al obtener la información sobre tu perro: {ex.Message}.");
				return View(info);
			}
		}

	}
}
