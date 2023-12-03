using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorAppDemo;
using BlazorAppDemo.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
var apiSettingsUrl = builder.Configuration.GetValue<string>("apiUrl");
builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri(apiSettingsUrl)
});
builder.Services.AddScoped<IProductService, BlazorAppDemo.Services.Product>();
builder.Services.AddScoped<ICategoryService, BlazorAppDemo.Services.Category>();

await builder.Build().RunAsync();
