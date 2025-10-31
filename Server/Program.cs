using Microsoft.Azure.Cosmos;
using SupportCosmos.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Tilføj controller + Razor pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// ✅ Hent Cosmos-konfiguration (fra appsettings.json + miljøvariabler)
var config = builder.Configuration.GetSection("CosmosDb");
string account = config["Account"];
string key = config["Key"];
string databaseName = config["DatabaseName"];
string containerName = config["ContainerName"];

// Debugging (valgfrit — kan hjælpe første gang du tester i Azure)
Console.WriteLine($"Cosmos endpoint: {account}");
Console.WriteLine($"Database: {databaseName}");
Console.WriteLine($"Container: {containerName}");

// ✅ Registrér CosmosClient som singleton
builder.Services.AddSingleton(sp => new CosmosClient(account, key));

// ✅ Registrér CosmosService (din egen klasse)
builder.Services.AddSingleton(sp =>
{
    var cosmosClient = sp.GetRequiredService<CosmosClient>();
    return new CosmosService(cosmosClient, databaseName, containerName);
});

var app = builder.Build();

// ✅ Produktion: brug HSTS og ExceptionHandler
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();