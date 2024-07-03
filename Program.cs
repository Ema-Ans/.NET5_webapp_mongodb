using Catalog.Repositories;
using Catalog.Settings;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => 
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Register your service
// Register your service
// we are trying to register and inject the mongodb client 
// into the repository we created for mongodb.

var mongoDbSettings = new MongoDbSettings
{
    Host = "localhost",
    Port = 27017
};

// Register MongoDB client
builder.Services.AddSingleton<IMongoClient>(_ =>
{
    return new MongoClient(mongoDbSettings.ConnectionString);
});


// builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
// {
//     var configuration = builder.Configuration;
//     var settings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
//     return new MongoClient(settings.ConnectionString);
// });

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));

BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));


builder.Services.AddSingleton<IInMemItemsRepository, MongoDbItemsRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     // app.UseSwagger();
//     // app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseCors(); // Use CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
