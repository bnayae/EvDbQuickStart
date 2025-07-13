using EvDb.Core;
using EvDbQuickStart.Funds.Events;
using EvDbQuickStart.Funds.Repositories;
using EvDbQuickStart.Funds.WebAPI;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Set up enums to be deserialized from strings
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(
        JsonNamingPolicy.CamelCase, allowIntegerValues: false));
});

// var context = EvDbStorageContext.CreateWithEnvironment("master", "evdb-quick-start", schema: "dbo"); // sql-server
// var context = EvDbStorageContext.CreateWithEnvironment("tests", "evdb-quick-start", schema: "public"); // postges
var context = EvDbStorageContext.CreateWithEnvironment("tests", "evdb-quick-start", schema: "default");
builder.Services.AddEvDb()
                .AddFundsFactory(o => o.UseMongoDBStoreForEvDbStream(), context)
                .DefaultSnapshotConfiguration(o => o.UseMongoDBForEvDbSnapshot());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/quick-start/{accountId}", async (IEvDbFundsFactory factory, Guid accountId) =>
{
    IEvDbFunds stream = await factory.GetAsync(accountId);
    var balance = stream.Views.Balance;
    return balance;
})
.WithOpenApi();

app.MapPost("/quick-start/", async (IEvDbFundsFactory factory, FunRequest request) =>
{
    // Consider to move this logic to a service layer
    IEvDbFunds stream = await factory.GetAsync(request.AccountId);
    if (request.Operation == OperationType.Deposit)
    {
        var deposit = new DepositedEvent { Amount = request.Amount, Attribution = request.Attribution };
        await stream.AppendAsync(deposit);
    }
    else if (request.Operation == OperationType.Withdraw)
    {
        var withdraw = new WithdrawnEvent(request.Amount) { Attribution = request.Attribution };
        await stream.AppendAsync(withdraw);
    }
    await stream.StoreAsync();
    var balance = stream.Views.Balance;
    return balance;
})
.WithOpenApi();

await app.RunAsync();

