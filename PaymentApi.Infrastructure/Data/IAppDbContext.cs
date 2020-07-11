using MongoDB.Driver;
using PaymentApi.Core.Models;

namespace PaymentApi.Infrastructure.Data
{
    public interface IAppDbContext
    {
        IMongoCollection<PaymentDocument> PaymentsCollection { get; }
    }
}
