using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DIVISOR_WASM.Client;
using DIVISOR_WASM.Client.Services.TeamService;
using DIVISOR_WASM.Client.Services.StudentService;
using Shared.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Services
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IStudentService, StudentService>();

await builder.Build().RunAsync();
