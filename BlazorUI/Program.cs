using AGV.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorUI;
using Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IAGVSimulationService, AgvSimulationService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<EventService>();


builder.Services.AddScoped(sp =>
    new HttpClient
    {
        /*BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)*/
        BaseAddress = new Uri("http://localhost:5004")
    });
await builder.Build().RunAsync();