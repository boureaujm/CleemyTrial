using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyCommons.Tools;
using CleemyInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleemyApplication.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public PaymentServices(IPaymentRepository paymentRepository,
            IUserRepository userRepository,
            ICurrencyRepository currencyRepository)
        {
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
        }

        public IEnumerable<Payment> GetByUserId(int userId)
        {
            Guard.IsNotZeroNegative(userId, "Must be positive");

            var payments = _paymentRepository.GetByUser(userId);

            return payments;
        }

        public async Task<Payment> Create(Payment newPayment)
        {
            Guard.IsNotNull(newPayment, "Must be not null");

            var user = _userRepository.GetById(newPayment.User.Id);
            Guard.IsNotNull(user, "user not exist");

            var currency = _currencyRepository.GetByCode(newPayment.Currency.Code);
            Guard.IsNotNull(currency, "currency not exist");

            if (currency.Code != user.AuthorizedCurrency.Code)
                throw new ArgumentException("payment currency and user currency must be the same");

            var createdPayment  = await _paymentRepository.CreateAsync(newPayment);

            return createdPayment;
        }
    }
}
