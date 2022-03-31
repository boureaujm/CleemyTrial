using CleemyCommons.Model;
using CleemyCommons.Types;
using CleemyInfrastructure;
using CleemyInfrastructure.entities;
using CleemyInfrastructure.entities.Adapter;
using CleemyInfrastructure.Repositories.internals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CleemyTests
{
    public class GetPaymentRepositoryTests
    {
        private readonly ApplicationContext _context;
        private readonly Mock<ILogger<PaymentRepository>> _logger;
        private readonly Payment2DbPaymentAdapter _adpater;
        private readonly List<DateTime> _dates;

        public GetPaymentRepositoryTests()
        {
            _dates = new List<DateTime>();
            _context = GetDatabaseContext();
            _logger = new Mock<ILogger<PaymentRepository>>();
            _adpater = new Payment2DbPaymentAdapter();
        }

        private ApplicationContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationContext(options);
            databaseContext.Database.EnsureCreated();

            var currency = databaseContext.Currencies.FirstOrDefault();

            var user = databaseContext.Users.FirstOrDefault();

            if (databaseContext.Payments.Count() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    var date = DateTime.Now.AddDays(i);
                    _dates.Add(date);
                    databaseContext.Payments.Add(new DbPayment()
                    {
                        Id = i,
                        Amount = 1000 - i,
                        Date = date,
                        User = user,
                        Currency = currency,
                        Comment = "mandatory",
                        PaymentNature = PaymentNatureEnum.Restaurant
                    });
                    databaseContext.SaveChanges();
                }
            }
            return databaseContext;
        }

        private void Reset()
        {
            _logger.Reset();
        }

        private PaymentRepository CreatePaymentRepository()
        {
            return new PaymentRepository(
                _adpater,
                _context,
                _logger.Object);
        }

        [Fact]
        [Trait("PaymentList.Repository", "Validation")]
        public void GetPayments_SortByAMountAsc_Ok()
        {
            Reset();

            PaymentRepository paymentRepository = CreatePaymentRepository();

            var sortWrapper = new SortWrapper
            {
                Field = PaymentConstants.CST_AMOUNT,
                Direction = CommonsConstants.CST_ASCENDING
            };

            var dbpayments = paymentRepository.GetByUser(userId: 1, sortWrapper);

            Assert.True(dbpayments.FirstOrDefault().Amount == 990);
        }

        [Fact]
        [Trait("PaymentList.Repository", "Validation")]
        public void GetPayments_SortByAMountDesc_Ok()
        {
            Reset();

            PaymentRepository paymentRepository = CreatePaymentRepository();

            var sortWrapper = new SortWrapper
            {
                Field = PaymentConstants.CST_AMOUNT,
                Direction = CommonsConstants.CST_DESCENDING
            };

            var dbpayments = paymentRepository.GetByUser(userId: 1, sortWrapper);

            Assert.True(dbpayments.FirstOrDefault().Amount == 999);
        }

        [Fact]
        [Trait("PaymentList.Repository", "Validation")]
        public void GetPayments_SortByDateAsc_Ok()
        {
            Reset();

            PaymentRepository paymentRepository = CreatePaymentRepository();

            var sortWrapper = new SortWrapper
            {
                Field = PaymentConstants.CST_DATE,
                Direction = CommonsConstants.CST_ASCENDING
            };

            var dbpayments = paymentRepository.GetByUser(userId: 1, sortWrapper);

            Assert.True(dbpayments.FirstOrDefault().Date == _dates.FirstOrDefault());
        }

        [Fact]
        [Trait("PaymentList.Repository", "Validation")]
        public void GetPayments_SortByDateDesc_Ok()
        {
            Reset();

            PaymentRepository paymentRepository = CreatePaymentRepository();

            var sortWrapper = new SortWrapper
            {
                Field = PaymentConstants.CST_DATE,
                Direction = CommonsConstants.CST_DESCENDING
            };

            var dbpayments = paymentRepository.GetByUser(userId: 1, sortWrapper);

            Assert.True(dbpayments.FirstOrDefault().Date == _dates.LastOrDefault());
        }
    }
}