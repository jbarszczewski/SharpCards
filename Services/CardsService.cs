using System.Net.Http.Json;
using SharpCards.Models;

namespace SharpCards.Services
{
	public class CardsService
	{
		public IList<Card> Cards { get; set; } = new List<Card>();
		private readonly HttpClient _http;
		public CardsService(HttpClient http)
		{
			_http = http;
			new Action(async () =>
			{
				Cards = (await _http.GetFromJsonAsync<Card[]>("sample-data/cards.json") ?? Enumerable.Empty<Card>()).ToList();
			})();

		}
		public async Task<Card[]> GetCards()
		{
			return await _http.GetFromJsonAsync<Card[]>("sample-data/cards.json") ?? Array.Empty<Card>();
		}
	}
}
