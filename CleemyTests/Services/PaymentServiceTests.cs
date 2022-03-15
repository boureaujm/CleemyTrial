using CleemyApplication.Services;
using CleemyCommons.Model;
using CleemyInfrastructure.entities;
using CleemyInfrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CleemyTests
{
    public class PaymentServiceTests
    {
        [Theory()]
        [InlineData(0)]
        [InlineData(-1)]
        public void GetPayments_ZeroNegativeUserId_ReturnException(int userId)        {

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(x => x.GetPayments(-1)).Returns(new List<Payment>().AsEnumerable() );

            var paymentServices = new PaymentServices(paymentRepository.Object);

            Assert.Throws<ArgumentException>(() => paymentServices.GetPayments(userId : userId));
        }

        [Fact]
        public void GetPayments_NotFoundUserId_ReturnEmpty()
        {

            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(x => x.GetPayments(-1)).Returns(new List<Payment>().AsEnumerable());

            var paymentServices = new PaymentServices(paymentRepository.Object);

            var payments = paymentServices.GetPayments(99);

            Assert.True(payments.Count() == 0);
        }
    }
}
