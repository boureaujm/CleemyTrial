using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyInfrastructure.entities;
using CleemyInfrastructure.entities.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleemyInfrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IEnumerableAdapter<DbPayment, Payment> _dbPaymentAdapter;
        private ApplicationContext _context;

        public PaymentRepository(
            IEnumerableAdapter<DbPayment, Payment> dbPaymentAdapter,
            ApplicationContext context            )
        {
            _dbPaymentAdapter = dbPaymentAdapter;
            _context = context;
        }

        public IEnumerable<Payment> GetPayments(int userId)
        {

            if (userId < 1)
                throw new ArgumentException();

            var dbPayments = _context.Payments.Where(p => p.User.Id == userId).ToList();

            var payments = _dbPaymentAdapter.Convert(dbPayments);

            return payments;
        }
    }
}
