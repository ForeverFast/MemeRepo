using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.Core;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<AppRoot>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddWebUI();

await builder.Build().RunAsync();
