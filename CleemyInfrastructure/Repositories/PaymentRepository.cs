using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyCommons.Tools;
using CleemyCommons.Types;
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
        private readonly IEnumerableAdapter<Payment, DbPayment> _paymentToDbPaymentAdapter;
        private ApplicationContext _context;
        private readonly ILogger<PaymentRepository> _logger;

        public PaymentRepository(
            IEnumerableAdapter<Payment, DbPayment> paymentToDbPaymentAdapter,
            ApplicationContext context,
            ILogger<PaymentRepository> logger
            )
        {
            _paymentToDbPaymentAdapter = paymentToDbPaymentAdapter;
            _context = context;
            _logger = logger;
        }

        public IEnumerable<DbPayment> GetByUser(int userId, SortWrapper sort)
        {

            if (userId < 1)
                throw new ArgumentException();

            try
            {
                var dbPayments = _context.Payments.Where(p => p.User.Id == userId);

                var direction = sort.Direction;

                switch (sort.Field)
                {
                    case PaymentConstants.CST_AMOUNT:
                        dbPayments = direction == CommonsConstants.CST_ASCENDING ? dbPayments.OrderBy(p => p.Amount) : dbPayments.OrderByDescending(p => p.Amount);
                        break;
                    case PaymentConstants.CST_DATE:
                        dbPayments = direction == CommonsConstants.CST_ASCENDING ? dbPayments.OrderBy(p => p.Date) : dbPayments.OrderByDescending(p => p.Date);
                        break;
                    default:
                        break;
                }

                return dbPayments.ToList();
            }
            catch (Exception err)
            {
                _logger.LogError("Get payments", err);
                return null;
            }

        }

        public bool CheckIfExists(Payment newPayment)
        {
            Guard.IsNotZeroNegative(newPayment, "Must be not null");

            var existingPayment = _context.Payments.Any(payment => payment.Amount == newPayment.Amount && payment.Date == newPayment.Date );

            return existingPayment;
        }

        public async Task<DbPayment> CreateAsync(Payment payment)
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
                return createdDbPayment;
            }
            catch (Exception err)
            {
                _logger.LogError("Create payment",err);
                return null;
            }
        }
    }
}
