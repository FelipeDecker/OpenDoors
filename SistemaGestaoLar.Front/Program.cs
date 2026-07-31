using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SistemaGestaoLar.Api.Client;
using SistemaGestaoLar.Api.Client.Contracts;
using SistemaGestaoLar.Front;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var gestaoLarApi = builder.Configuration["GestaoLarApi"]
    ?? throw new InvalidOperationException("A configuração 'GestaoLarApi' é obrigatória.");

if (!Uri.TryCreate(gestaoLarApi, UriKind.Absolute, out var gestaoLarApiUri))
{
    throw new InvalidOperationException("A configuração 'GestaoLarApi' deve conter uma URL absoluta válida.");
}

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = gestaoLarApiUri });
builder.Services.AddScoped<IAjudantesClient>(sp => new AjudantesClient(gestaoLarApi, sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<IGruposClient>(sp => new GruposClient(gestaoLarApi, sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<IMoradoresClient>(sp => new MoradoresClient(gestaoLarApi, sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<ITicketDiariosClient>(sp => new TicketDiariosClient(gestaoLarApi, sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<IEnumsClient>(sp => new EnumsClient(gestaoLarApi, sp.GetRequiredService<HttpClient>()));

await builder.Build().RunAsync();
