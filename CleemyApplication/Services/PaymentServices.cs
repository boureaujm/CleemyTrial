using CleemyCommons.Model;
using CleemyInfrastructure.Repositories;
using System;
using System.Collections.Generic;

namespace CleemyApplication.Services
{
    public class PaymentServices
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentServices(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;

        }

        public IEnumerable<Payment> GetPayments(int userId) {
            
            if (userId < 1)
                throw new ArgumentException();

            var payments = _paymentRepository.GetPayments(userId);
            return payments;
        }
    }
}
