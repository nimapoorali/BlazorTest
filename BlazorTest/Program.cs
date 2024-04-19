using Blazored.LocalStorage;
using BlazorTest;
using BlazorTest.Helpers;
using BlazorTest.Infrastructure;
using BlazorTest.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5000/gateway/v1/") });

//Registeration of ApiHelper to retreive data from backend apis
builder.Services.AddScoped<ApiHelper>();

//Registeration of services used in components
builder.Services.RegisterServices();

//Registeration of LocalStorage
builder.Services.AddBlazoredLocalStorage();

//Authentication custom requirements registeration
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

//Registeration of MudBlazor
builder.Services.AddMudServices();

await builder.Build().RunAsync();
