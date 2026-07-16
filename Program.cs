using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TiempoBiblia.Client;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Conectamos el cliente directamente a tu API en producción
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://tiempobibliaappapi-hbb2bzgvc4fudkcw.canadacentral-01.azurewebsites.net/") });

// INYECTA MUDBLAZOR AQUÍ
builder.Services.AddMudServices(); 

await builder.Build().RunAsync();