using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyCommons.Tools;
using CleemyInfrastructure.Repositories;
using System;
using System.Collections.Generic;

namespace CleemyApplication.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentServices(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;

        }

        public IEnumerable<Payment> GetPayments(int userId)
        {
            Guard.IsNotNull(userId, "Must be not null");

            var payments = _paymentRepository.GetPayments(userId);

            return payments;
        }
    }
}
