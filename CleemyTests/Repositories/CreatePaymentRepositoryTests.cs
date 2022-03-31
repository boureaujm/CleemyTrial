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
using System.Linq;
using Xunit;

namespace CleemyTests
{
    public class CreatePaymentRepositoryTests
    {
        private readonly ApplicationContext _context;
        private readonly Mock<ILogger<PaymentRepository>> _logger;
        private readonly Payment2DbPaymentAdapter _adpater;
        private readonly DateTime _now;

        public CreatePaymentRepositoryTests()
        {
            _now = DateTime.Now;
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
                databaseContext.Payments.Add(new DbPayment()
                {
                    Id = 1,
                    Amount = 1000.75,
                    Date = _now,
                    User = user,
                    Currency = currency,
                    Comment = "mandatory",
                    PaymentNature = PaymentNatureEnum.Restaurant
                });
                databaseContext.SaveChanges();
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
        [Trait("PaymentCReate.Repository", "DuplicateTest")]
        public void CreatePayment_CheckIfDuplicate_returnTrue()
        {
            Reset();

            PaymentRepository paymentRepository = CreatePaymentRepository();

            var payment = new Payment()
            {
                Id = 1,
                Amount = 1000.75,
                Date = _now,
                User = new User { Id = 1 },
                Currency = new Currency
                {
                    Code = "USD"
                },
                Comment = "mandatory",
                PaymentNature = PaymentNatureEnum.Restaurant
            };

            var exists = paymentRepository.CheckIfExists(payment);

            Assert.True(exists);
        }

        [Fact]
        [Trait("PaymentCReate.Repository", "DuplicateTest")]
        public void CreatePayment_CheckIfDuplicate_returnFalse()
        {
            Reset();

            PaymentRepository paymentRepository = CreatePaymentRepository();

            var payment = new Payment()
            {
                Id = 1,
                Amount = 1000.75,
                Date = _now,
                User = new User { Id = 2 },
                Currency = new Currency
                {
                    Code = "USD"
                },
                Comment = "mandatory",
                PaymentNature = PaymentNatureEnum.Restaurant
            };

            var exists = paymentRepository.CheckIfExists(payment);

            Assert.False(exists);
        }

        [Fact]
        [Trait("PaymentCReate.Repository", "DuplicateTest")]
        public void CreatePayment_Create_returnPayment()
        {
            Reset();

            PaymentRepository paymentRepository = CreatePaymentRepository();

            var payment = new Payment()
            {
                Id = 1,
                Amount = 1000.75,
                Date = _now.AddDays(-3),
                User = new User { Id = 1 },
                Currency = new Currency
                {
                    Code = "USD"
                },
                Comment = "mandatory",
                PaymentNature = PaymentNatureEnum.Misc
            };

            var dbPayment = paymentRepository.CreateAsync(payment);

            Assert.True(dbPayment.Id != 0);
        }
    }
}