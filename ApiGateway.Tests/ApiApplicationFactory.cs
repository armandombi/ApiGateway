using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using PaymentApi.Core.Interfaces;
using PaymentApi.Infrastructure.Data;
using PaymentApi.Infrastructure.Services;

namespace ApiGateway.Tests
{
    public class ApiApplicationFactory : WebApplicationFactory<PaymentApi.Startup>
    {
        private static MongoDbRunner _runner;
        private static MongoClient _client;
        private static IMongoDatabase _fakeDb;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var bankApi = new WebApplicationFactory<BankApiSimulator.Startup>();
            var bankClient = bankApi.CreateClient();
            _runner = MongoDbRunner.Start();
            _client = new MongoClient(_runner.ConnectionString);
            _fakeDb = _client.GetDatabase("IntegrationTestDb");
            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<IAcquiringBankService>(s => new AcquiringBankService(bankClient));
                services.AddSingleton(_client).AddSingleton(_fakeDb).AddSingleton<IAppDbContext, MongoDbContext>();
            });
        }
    }
}
