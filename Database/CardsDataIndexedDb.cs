using DnetIndexedDb;
using Microsoft.JSInterop;

namespace SharpCards.Database;

public class CardsDataIndexedDb : IndexedDbInterop
{
	public CardsDataIndexedDb(IJSRuntime jsRuntime, IndexedDbOptions<CardsDataIndexedDb> options)
		: base(jsRuntime, options)
	{
	}
}