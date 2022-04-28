using System.Net.Http.Json;
using SharpCards.Models;

namespace SharpCards.Services
{
	public class CardsService
	{
		private readonly HttpClient _http;
		public CardsService(HttpClient http)
		{
			_http = http;
		}
		public async Task<Card[]> GetCards()
		{
			return await _http.GetFromJsonAsync<Card[]>("sample-data/cards.json") ?? Array.Empty<Card>();
		}
	}
}