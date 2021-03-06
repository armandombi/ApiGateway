﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Driver;
using Newtonsoft.Json;
using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Models;
using PaymentApi.Core.Models.Enums;
using PaymentApi.Infrastructure.Data;
using Serilog;

namespace PaymentApi.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAcquiringBankService _bankService;
        private readonly IAppDbContext _db;

        public PaymentService(IAppDbContext db, IAcquiringBankService bankService)
        {
            _db = db;
            _bankService = bankService;
        }

        /// <summary>
        ///     Method to process a payment from a payment request.
        /// </summary>
        /// <param name="id">The payment unique identifier</param>
        /// <param name="request">The information about the payment</param>
        /// <returns>True if the payment already exists or it's successfully processed or False if the payment fails</returns>
        public async Task<bool> ProcessPayment(Guid id, PaymentRequest request)
        {
            try
            {
                Log.Information($"Processing payment {id}");
                var paymentDocument =
                    await (await _db.PaymentsCollection.FindAsync(x => x.Id == id)).FirstOrDefaultAsync();

                if (paymentDocument == null)
                {
                    Log.Debug($"Sending payment {id} to the bank");
                    var bankResponse = await _bankService.SendPayment(request);
                    Log.Debug($"Received payment {id} from the bank");

                    paymentDocument = new PaymentDocument
                    {
                        Id = id,
                        Amount = request.Amount,
                        BankPaymentId = bankResponse.Id,
                        CardNumber = request.CardNumber,
                        CardHolderName = request.CardHolderName,
                        Created = DateTimeOffset.UtcNow,
                        Status = bankResponse.Status == TransactionStatus.Complete
                            ? PaymentStatus.Complete
                            : PaymentStatus.Failed,
                        Currency = request.Currency
                    };

                    await _db.PaymentsCollection.InsertOneAsync(paymentDocument);
                }

                return paymentDocument.Status != PaymentStatus.Failed;
            }
            catch (Exception exception)
            {
                Log.Error(
                    $"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                throw;
            }
        }

        /// <summary>
        ///     Method to retrieve and return a payment details based on the unique payment identifier
        /// </summary>
        /// <param name="id">The payment unique identifier</param>
        /// <returns>The payment details or a null object if the payment is not found</returns>
        public async Task<PaymentDocument> GetPayment(Guid id)
        {
            return await (await _db.PaymentsCollection.FindAsync(c => c.Id == id)).FirstOrDefaultAsync();
        }
    }
}