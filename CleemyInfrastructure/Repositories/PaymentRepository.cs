using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyCommons.Tools;
using CleemyInfrastructure.entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleemyInfrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IEnumerableAdapter<DbPayment, Payment> _dbPaymentToPaymentAdapter;
        private readonly IEnumerableAdapter<Payment, DbPayment> _paymentToDbPaymentAdapter;
        private ApplicationContext _context;
        private readonly ILogger<PaymentRepository> _logger;

        public PaymentRepository(
            IEnumerableAdapter<DbPayment, Payment> dbPaymentToPaymentAdapter,
            IEnumerableAdapter<Payment, DbPayment> paymentToDbPaymentAdapter,
            ApplicationContext context,
            ILogger<PaymentRepository> logger
            )
        {
            _dbPaymentToPaymentAdapter = dbPaymentToPaymentAdapter;
            _paymentToDbPaymentAdapter = paymentToDbPaymentAdapter;
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Payment> GetByUser(int userId)
        {

            if (userId < 1)
                throw new ArgumentException();

            try
            {
                var dbPayments = _context.Payments.Where(p => p.User.Id == userId).ToList();
                var payments = _dbPaymentToPaymentAdapter.Convert(dbPayments);
                return payments;
            }
            catch (Exception err)
            {
                _logger.LogError("Get payments", err);
                return null;
            }

        }

        public async Task<Payment> CreateAsync(Payment payment)
        {
            Guard.IsNotNull(payment, "payment must be not null");

            try
            {
                var newDbPayment = _paymentToDbPaymentAdapter.Convert(payment);

                var dbCurrency = _context.Currencies.FirstOrDefault(c => c.Code == payment.Currency.Code);
                var dbUser = _context.Users.FirstOrDefault(u => u.Id == payment.User.Id);

                _context.Payments.Add(newDbPayment);                
                await _context.SaveChangesAsync();

                var createdDbPayment = await _context.Payments.FindAsync(newDbPayment.Id);
                var newPayment = _dbPaymentToPaymentAdapter.Convert(createdDbPayment);
                return newPayment;
            }
            catch (Exception err)
            {
                _logger.LogError("Create payment",err);
                return null;
            }
        }
    }
}
