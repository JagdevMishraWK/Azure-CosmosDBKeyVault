using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Cosmos;
using ShoppingWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureAppConfiguration((context, config) => {
    var settings = config.Build();
    var keyVaultURL = settings["KeyVaultConfiguration:KeyVaultURL"];
    var clientId = settings["KeyVaultConfiguration:ClientId"];
    var tenantId = settings["KeyVaultConfiguration:TenantId"];
    var clientSecret = settings["KeyVaultConfiguration:ClientSecret"];

    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
    var client=new SecretClient(new Uri(keyVaultURL), credential);
    config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

    //builder.Services.AddSingleton<ITodoService>(options =>
    //{
    //    string url = client.GetSecret("dev-cosmos-url").Value.Value.ToString();
    //    string primaryKey = client.GetSecret("dev-cosmos-primaryKey").Value.Value.ToString();
    //    string dbName = builder.Configuration.GetSection("AzureCosmosDbSettings").GetValue<string>("DatabaseName");
    //    string containerName = builder.Configuration.GetSection("AzureCosmosDbSettings").GetValue<string>("ContainerName");
    //    var cosmosClient = new CosmosClient(url, primaryKey);
    //    return new TodoService(cosmosClient, dbName, containerName);
    //});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITodoService>(options =>
{
    string url = builder.Configuration.GetSection("AzureCosmosDbSettings").GetValue<string>("URL");
    string primaryKey = builder.Configuration.GetSection("AzureCosmosDbSettings").GetValue<string>("PrimaryKey");
    string dbName = builder.Configuration.GetSection("AzureCosmosDbSettings").GetValue<string>("DatabaseName");
    string containerName = builder.Configuration.GetSection("AzureCosmosDbSettings").GetValue<string>("ContainerName");
    var cosmosClient = new CosmosClient(url, primaryKey);
    return new TodoService(cosmosClient, dbName, containerName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();