using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using SharpCards.Database;
using SharpCards.Models;

namespace SharpCards.Services;

public class CardsService
{
	private readonly CardsDataIndexedDb _database;
	private readonly HttpClient _http;

	public CardsService(HttpClient http, CardsDataIndexedDb database)
	{
		_http = http;
		_database = database;
	}
	public bool IsInitialized { get; private set; }
	public IList<Card> Cards { get; private set; } = new List<Card>();
	public async ValueTask Init()
	{
		_ = await _database.OpenIndexedDb();
		var cards = await _database.GetAll<CardDto>();
		Cards = cards.Select(Card.FromDto).ToList();
		IsInitialized = true;
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
		_ = await _database.AddItems(staticCards);
	}

	public async ValueTask AddCard(Card newCard)
	{
		_ = await _database.AddItems(new List<CardDto> { newCard.ToDto() });
		Cards.Add(newCard);
	}

	public async ValueTask DeleteCard(Guid cardGuid)
	{
		_ = await _database.DeleteByKey<string, CardDto>(cardGuid.ToString());
		Cards.RemoveAt(Cards.IndexOf(Cards.First(c => c.Id == cardGuid)));
	}

	public Stream GetCardsStream()
	{
		var jsonString = JsonSerializer.Serialize(Cards);
		return new MemoryStream(Encoding.Default.GetBytes(jsonString));
	}

	public async Task AddCardsFromJson(string input)
	{
		var cards = JsonSerializer.Deserialize<IList<CardDto>>(input) ?? Enumerable.Empty<CardDto>();
		var newCards = cards.Where(c => Cards.All(c2 => c.Id != c2.Id)).ToList();
		_ = await _database.AddItems<CardDto>(newCards);
		foreach (var card in newCards)
		{
			Cards.Add(Card.FromDto(card));
		}

		Console.WriteLine($"{newCards.Count} new cards added");
	}
}