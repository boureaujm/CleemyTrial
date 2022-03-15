using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyInfrastructure.entities;
using CleemyInfrastructure.entities.Adapter;
using System;
using System.Collections.Generic;

namespace CleemyInfrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IEnumerableAdapter<DbPayment, Payment> _dbPaymentAdapter;

        public PaymentRepository(IEnumerableAdapter<DbPayment, Payment> dbPaymentAdapter)
        {
            _dbPaymentAdapter = dbPaymentAdapter;
        }

        public IEnumerable<Payment> GetPayments(int userId)
        {

            if (userId < 1)
                throw new ArgumentException();

            var dbPayments = new List<DbPayment>();

            var payments = _dbPaymentAdapter.Convert(dbPayments);

            return payments;
        }
    }
}
