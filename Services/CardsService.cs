using System.Net.Http.Json;
using SharpCards.Database;
using SharpCards.Models;

namespace SharpCards.Services;

public class CardsService
{
	private readonly HttpClient _http;
	private readonly CardsDataIndexedDb database;

	public CardsService(HttpClient http, CardsDataIndexedDb database)
	{
		_http = http;
		this.database = database;
		new Action(async () =>
		{
			Cards = await Init();
		})();

	}
	public IList<Card> Cards { get; private set; } = new List<Card>();
	public async ValueTask<IList<Card>> Init()
	{
		_ = await database.OpenIndexedDb();
		var cards = await database.GetAll<CardDto>();
		return cards.Select(c => Card.FromDto(c)).ToList();
	}

	public async ValueTask InitWithStatic()
	{
		var staticCards =
			(await _http.GetFromJsonAsync<CardDto[]>("sample-data/cards.json") ?? Enumerable.Empty<CardDto>()).ToList();
		var newCards = staticCards.Where(c => Cards.All(c2 => c.Id != c2.Id));
		_ = newCards.Select(r =>
		{
			Console.WriteLine(r.Id);
			return r;
		});
		_ = await database.AddItems(staticCards);
	}
}