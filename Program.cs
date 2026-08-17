using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TiempoBiblia.Client;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🔥 1. LEEMOS LA URL DESDE appsettings.json
var apiUrl = builder.Configuration["ApiSettings:BaseUrl"] 
             ?? "https://localhost:7147/"; // URL de respaldo por si falla la lectura

// 🔥 2. INYECTAMOS EL HTTP CLIENT LIMPIO
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
builder.Services.AddScoped<TiempoBiblia.Client.Services.CarritoService>();

// INYECTA MUDBLAZOR AQUÍ
builder.Services.AddMudServices(); 

await builder.Build().RunAsync();