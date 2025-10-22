using Microsoft.Azure.Cosmos;
using SupportCosmos.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// ✅ Registrér CosmosClient
builder.Services.AddSingleton(sp =>
{
    var config = builder.Configuration;
    return new CosmosClient(
        config["CosmosDb:Account"],
        config["CosmosDb:Key"]
    );
});

// ✅ Registrér CosmosService med databaseName og containerName
builder.Services.AddSingleton(sp =>
{
    var config = builder.Configuration;
    var cosmosClient = sp.GetRequiredService<CosmosClient>();
    return new CosmosService(
        cosmosClient,
        config["CosmosDb:DatabaseName"],
        config["CosmosDb:ContainerName"]
    );
});

var app = builder.Build();

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