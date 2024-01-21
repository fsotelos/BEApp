using CurrieTechnologies.Razor.SweetAlert2;
using HealthyBlazor;
using HealthyBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://samplecrudall.azurewebsites.net/") });
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSweetAlert2();
await builder.Build().RunAsync();
