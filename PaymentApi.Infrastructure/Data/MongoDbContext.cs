using MongoDB.Driver;
using PaymentApi.Core.Models;

namespace PaymentApi.Infrastructure.Data
{
    public class MongoDbContext : IAppDbContext
    {
        public MongoDbContext(IMongoDatabase db)
        {
            PaymentsCollection = db.GetCollection<PaymentDocument>("payments");
        }

        public IMongoCollection<PaymentDocument> PaymentsCollection { get; }
    }
}