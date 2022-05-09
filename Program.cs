using DnetIndexedDb;
using DnetIndexedDb.Fluent;
using DnetIndexedDb.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharpCards;
using SharpCards.Database;
using SharpCards.Services;

WebAssemblyHostBuilder? builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<CardsService>();
builder.Services.AddIndexedDbDatabase<CardsDataIndexedDb>(options =>
   {
	   global::DnetIndexedDb.Models.IndexedDbDatabaseModel? model = new IndexedDbDatabaseModel()
		   .WithName("Cards")
		   .WithVersion(1)
		   .WithModelId(0);

	   _ = model.AddStore<CardDto>();
	   _ = options.UseDatabase(model);
   }, ServiceLifetime.Singleton);

await builder.Build().RunAsync();
