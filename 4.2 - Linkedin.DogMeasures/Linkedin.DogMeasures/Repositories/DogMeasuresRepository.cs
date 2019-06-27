using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkedin.DogMeasures.Contracts;
using Newtonsoft.Json;

namespace Linkedin.DogMeasures.Repositories
{
	public class DogMeasuresRepository : IDogMeasuresRepository
	{

		private readonly List<Models.DogMeasures> _measures;

		public DogMeasuresRepository()
		{
			string jsonFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DogMeasures.json");
			_measures = JsonConvert.DeserializeObject<List<Models.DogMeasures>>(File.ReadAllText(jsonFile, Encoding.UTF8));
		}

		public Models.DogMeasures GetMeasures (string breed)
		{
			return _measures.SingleOrDefault(r => r.Breed.Equals(breed, StringComparison.InvariantCultureIgnoreCase));
		}

	}
}