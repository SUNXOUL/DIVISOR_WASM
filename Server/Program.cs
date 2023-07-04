using Microsoft.AspNetCore.ResponseCompression;
using DIVISOR_WASM.Server.Services.StudentService;
using DIVISOR_WASM.Server.Services.TeamService;
using Microsoft.EntityFrameworkCore;
using DIVISOR_WASM.Server.DAL;
var builder = WebApplication.CreateBuilder(args);

//Contexto
var ConStr = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContext<Contexto>(Options => Options.UseSqlite(ConStr));

// Add services to the container.

builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
