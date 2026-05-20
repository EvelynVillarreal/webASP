using System.Globalization;
using Microsoft.AspNetCore.Localization;
using MongoDB.Driver;
using MongoDB.Entities;
using ProductosMVC.Services;
using ProductosMVC.Settings;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://+:{port}");

var mongoSettings = builder.Configuration
    .GetSection(MongoDbSettings.ConfigurationSection)
    .Get<MongoDbSettings>();

var clientSettings = MongoClientSettings.FromConnectionString(mongoSettings!.ConnectionString);
await DB.InitAsync(mongoSettings.DatabaseName, clientSettings);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Product/Error");
    app.UseHsts();
}

var supportedCultures = new[] { new CultureInfo("en-US") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
app.UseRequestLocalization(localizationOptions);

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
