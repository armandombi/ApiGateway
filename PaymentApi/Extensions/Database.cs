using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PaymentApi.Infrastructure.Data;

namespace PaymentApi.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            if (null == BsonSerializer.SerializerRegistry.GetSerializer<decimal>())
                BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));

            return services.AddSingleton(ctx =>
                {
                    var connStr = configuration.GetConnectionString("Mongo");
                    return new MongoClient(connStr);
                })
                .AddSingleton(ctx =>
                {
                    var dbName = configuration.GetValue<string>("DbName") ?? throw new ArgumentNullException();
                    var client = ctx.GetRequiredService<MongoClient>();
                    var database = client.GetDatabase(dbName);
                    return database;
                }).AddSingleton<IAppDbContext, MongoDbContext>();
        }
    }
}
