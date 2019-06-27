using System;
using System.Threading.Tasks;
using Linkedin.DogMeasures.Models;
using Linkedin.DogMeasures.Services;
using Microsoft.AspNetCore.Mvc;

namespace Linkedin.DogMeasures.Controllers
{
	public class HomeController : Controller
	{

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
				var measures = new DogMeasuresService().CheckDogIdealWeight(info.Breed, info.Weight);
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
