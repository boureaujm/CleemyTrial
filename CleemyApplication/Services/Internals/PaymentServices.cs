using CleemyCommons.Exceptions;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyCommons.Tools;
using CleemyCommons.Types;
using CleemyInfrastructure.entities;
using CleemyInfrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleemyApplication.Services.internals
{
    internal class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IEnumerableAdapter<DbPayment, Payment> _dbPaymentToPaymentAdapter;

        public PaymentServices(
            IPaymentRepository paymentRepository,
            IUserRepository userRepository,
            ICurrencyRepository currencyRepository,
            IEnumerableAdapter<DbPayment, Payment> dbPaymentToPaymentAdapter)
        {
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _dbPaymentToPaymentAdapter = dbPaymentToPaymentAdapter;
        }

        public IEnumerable<Payment> GetByUserId(int userId, SortWrapper sortWrapper)
        {
            Guard.IsNotZeroNegative(userId, "Must be positive");

            var dbPayments = _paymentRepository.GetByUser(userId, sortWrapper);
            var payments = _dbPaymentToPaymentAdapter.Convert(dbPayments.ToList());
            return payments;

            return payments;
        }

        public async Task<Payment> Create(Payment newPayment)
        {
            Guard.IsNotNull(newPayment, "Must be not null");

            var user = _userRepository.GetById(newPayment.User.Id);
            if (user is null)
                throw new UserNotExistException(PaymentConstants.CST_ERROR_USER_NOT_EXIST);

            var currency = _currencyRepository.GetByCode(newPayment.Currency.Code);
            if (currency is null)
                throw new CurrencyNotExistException(PaymentConstants.CST_ERROR_CURRENCY_NOT_EXIST);

            if (currency.Code != user.AuthorizedCurrency.Code)
                throw new CurrencyIncoherenceException(PaymentConstants.CST_ERROR_INCOHERENT_CURRENCY);

            var existingPayment = _paymentRepository.CheckIfExists(newPayment);
            if (existingPayment == true)
                throw new DuplicatePaymentException(PaymentConstants.CST_ERROR_DUPLICATE_PAYMENT);

            var createdDbPayment = await _paymentRepository.CreateAsync(newPayment);

            var createdPayment = _dbPaymentToPaymentAdapter.Convert(createdDbPayment);

            return createdPayment;
        }
    }
}