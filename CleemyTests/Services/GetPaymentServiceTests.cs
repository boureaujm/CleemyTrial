using CleemyApplication.Services;
using CleemyCommons.Model;
using CleemyCommons.Types;
using CleemyInfrastructure.entities;
using CleemyInfrastructure.entities.Adapter;
using CleemyInfrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CleemyTests
{
    public class GetPaymentServiceTests
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<ICurrencyRepository> _currencyRepository;
        private Mock<IPaymentRepository> _paymentRepository;
        private readonly SortWrapper _sortWrapper;

        public GetPaymentServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _currencyRepository = new Mock<ICurrencyRepository>();
            _paymentRepository = new Mock<IPaymentRepository>();

            _sortWrapper = new SortWrapper
            {
                Field = PaymentConstants.CST_AMOUNT,
                Direction = CommonsConstants.CST_ASCENDING
            };
        }

        private void Reset()
        {
            _userRepository.Reset();
            _currencyRepository.Reset();
            _paymentRepository.Reset();
        }

        private PaymentServices CreatePaymentService()
        {
            var adatpter = new DbPayment2PaymentAdapter();
            return new PaymentServices(
                _paymentRepository.Object,
                _userRepository.Object,
                _currencyRepository.Object,
                adatpter);
        }

        [Theory()]
        [InlineData(0)]
        [InlineData(-1)]
        [Trait("PaymentList.Service", "Validation")]
        public void GetPayments_ZeroNegativeUserId_ReturnException(int userId)
        {
            Reset();

            _paymentRepository.Setup(x => x.GetByUser(-1, _sortWrapper)).Returns(new List<DbPayment>().AsEnumerable());

            PaymentServices paymentServices = CreatePaymentService();

            Assert.Throws<ArgumentException>(() => paymentServices.GetByUserId(userId: userId, _sortWrapper));
        }

        [Fact]
        [Trait("PaymentList.Service", "Validation")]
        public void GetPayments_NotFoundUserId_ReturnEmptyAsync()
        {
            Reset();
            
            _paymentRepository.Setup(x => x.GetByUser(-1, _sortWrapper)).Returns(new List<DbPayment>().AsEnumerable());

            PaymentServices paymentServices = CreatePaymentService();

            var payments = paymentServices.GetByUserId(99, _sortWrapper);

            Assert.True(payments.Count() == 0);
        }
    }
}