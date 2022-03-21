using CleemyApplication.Services;
using CleemyCommons.Model;
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

        public GetPaymentServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _currencyRepository = new Mock<ICurrencyRepository>();
            _paymentRepository = new Mock<IPaymentRepository>();
        }

        private void Reset()
        {
            _userRepository.Reset();
            _currencyRepository.Reset();
            _paymentRepository.Reset();
        }

        private PaymentServices CreatePaymentService()
        {
            return new PaymentServices(
                _paymentRepository.Object,
                _userRepository.Object,
                _currencyRepository.Object);
        }

        [Theory()]
        [InlineData(0)]
        [InlineData(-1)]
        public void GetPayments_ZeroNegativeUserId_ReturnException(int userId)
        {
            Reset();

            _paymentRepository.Setup(x => x.GetByUser(-1)).Returns(new List<Payment>().AsEnumerable());

            PaymentServices paymentServices = CreatePaymentService();

            Assert.Throws<ArgumentException>(() => paymentServices.GetByUserId(userId: userId));
        }

        [Fact]
        public void GetPayments_NotFoundUserId_ReturnEmptyAsync()
        {
            Reset();
            
            _paymentRepository.Setup(x => x.GetByUser(-1)).Returns(new List<Payment>().AsEnumerable());

            PaymentServices paymentServices = CreatePaymentService();

            var payments = paymentServices.GetByUserId(99);

            Assert.True(payments.Count() == 0);
        }
    }
}