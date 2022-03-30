using CleemyApplication.Services;
using CleemyApplication.Services.internals;
using CleemyCommons.Exceptions;
using CleemyCommons.Model;
using CleemyCommons.Types;
using CleemyInfrastructure.entities.Adapter;
using CleemyInfrastructure.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CleemyTests
{
    public class CreatePaymentServiceTests
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<ICurrencyRepository> _currencyRepository;
        private Mock<IPaymentRepository> _paymentRepository;

        public CreatePaymentServiceTests()
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
            var adatpter = new DbPayment2PaymentAdapter();
            return new PaymentServices(
                _paymentRepository.Object,
                _userRepository.Object,
                _currencyRepository.Object,
                adatpter);
        }

        [Fact]
        [Trait("PaymentCreate.Service", "Validation")]
        public async Task CreatePayments_Null_ReturnExceptionAsync()
        {
            Reset();
            PaymentServices paymentServices = CreatePaymentService();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await paymentServices.Create(null));
        }

        [Fact]
        [Trait("PaymentCreate.Service", "Validation")]
        public async Task CreatePayment_PaymentValid_ReturnNewPayment()
        {
            var user = new User
            {
                FirstName = "test",
                LastName = "test",
                AuthorizedCurrency = new Currency
                {
                    Code = "USD"
                },
                Id = 9
            };

            var currency = new Currency
            {
                Code = "USD"
            };

            var newPayment = new Payment
            {
                Amount = 10,
                Comment = "create",
                Currency = currency,
                Date = DateTime.Now,
                PaymentNature = CleemyCommons.Types.PaymentNatureEnum.Restaurant,
                User = user,
                Id = 1
            };

            Reset();

            _userRepository.Setup(x => x.GetById(9)).Returns(user);
            _currencyRepository.Setup(x => x.GetByCode("USD")).Returns(currency);
            _paymentRepository.Setup(x => x.CreateAsync(newPayment)).Returns(Task.FromResult(
                new CleemyInfrastructure.entities.DbPayment()
                {
                    Id = 1,
                    Amount = 1000.75,
                    Date = DateTime.Now,
                    User = new CleemyInfrastructure.entities.DbUser { Id = 1 ,
                    FirstName = "test",
                    LastName = "test"},
                    Currency = new CleemyInfrastructure.entities.DbCurrency
                    {
                        Code = "USD",
                        Label = "US Dollar"
                    },
                    Comment = "mandatory",
                    PaymentNature = PaymentNatureEnum.Misc
                }
             ));

            PaymentServices paymentServices = CreatePaymentService();

            var created = await paymentServices.Create(newPayment);

            Assert.NotNull(created);
            Assert.True(created.Id == 1);
        }

        [Fact]
        [Trait("PaymentCreate.Service", "Validation")]
        public async Task CreatePayment_InvalidUser_ThrowException()
        {
            var user = new User
            {
                FirstName = "test",
                LastName = "test",
                AuthorizedCurrency = new Currency
                {
                    Code = "USD"
                },
                Id = 9
            };

            var currency = new Currency
            {
                Code = "FFR"
            };

            var newPayment = new Payment
            {
                Amount = 10,
                Comment = "create",
                Currency = currency,
                Date = DateTime.Now,
                PaymentNature = CleemyCommons.Types.PaymentNatureEnum.Restaurant,
                User = user,
                Id = 1
            };

            Reset();

            _userRepository.Setup(x => x.GetById(9)).Returns((User)null);

            PaymentServices paymentServices = CreatePaymentService();

            await Assert.ThrowsAsync<UserNotExistException>(async () => await paymentServices.Create(newPayment));
        }

        [Fact]
        [Trait("PaymentCreate.Service", "Validation")]
        public async Task CreatePayment_InvalidCurrency_ThrowException()
        {
            var user = new User
            {
                FirstName = "test",
                LastName = "test",
                AuthorizedCurrency = new Currency
                {
                    Code = "USD"
                },
                Id = 9
            };

            var invalidCurrency = new Currency
            {
                Code = "FFR"
            };

            var newPayment = new Payment
            {
                Amount = 10,
                Comment = "create",
                Currency = invalidCurrency,
                Date = DateTime.Now,
                PaymentNature = CleemyCommons.Types.PaymentNatureEnum.Restaurant,
                User = user,
                Id = 1
            };

            Reset();

            _userRepository.Setup(x => x.GetById(9)).Returns(user);
            _currencyRepository.Setup(x => x.GetByCode("FFR")).Returns((Currency)null);

            PaymentServices paymentServices = CreatePaymentService();

            await Assert.ThrowsAsync<CurrencyNotExistException>(async () => await paymentServices.Create(newPayment));
        }

        [Fact]
        [Trait("PaymentCreate.Service", "Validation")]
        public async Task CreatePayment_IncoherentCurrency_ThrowException()
        {
            var user = new User
            {
                FirstName = "test",
                LastName = "test",
                AuthorizedCurrency = new Currency
                {
                    Code = "USD"
                },
                Id = 9
            };

            var incoherentCurrency = new Currency
            {
                Code = "RUB"
            };

            var newPayment = new Payment
            {
                Amount = 10,
                Comment = "create",
                Currency = incoherentCurrency,
                Date = DateTime.Now,
                PaymentNature = CleemyCommons.Types.PaymentNatureEnum.Restaurant,
                User = user,
                Id = 1
            };

            Reset();

            _userRepository.Setup(x => x.GetById(9)).Returns(user);
            _currencyRepository.Setup(x => x.GetByCode("RUB")).Returns(incoherentCurrency);
            _paymentRepository.Setup(x => x.CreateAsync(newPayment)).Returns(Task.FromResult(new CleemyInfrastructure.entities.DbPayment { Id = 1 }));

            PaymentServices paymentServices = CreatePaymentService();

            await Assert.ThrowsAsync<CurrencyIncoherenceException>(async () => await paymentServices.Create(newPayment));
        }

        [Fact]
        [Trait("PaymentCreate.Service", "Validation")]
        public async Task CreatePayment_DuplicatePayment_ThrowException()
        {
            var user = new User
            {
                FirstName = "test",
                LastName = "test",
                AuthorizedCurrency = new Currency
                {
                    Code = "USD"
                },
                Id = 1
            };

            var currency = new Currency
            {
                Code = "USD"
            };

            var newPayment = new Payment
            {
                Amount = 10,
                Comment = "create",
                Currency = currency,
                Date = DateTime.Now,
                PaymentNature = CleemyCommons.Types.PaymentNatureEnum.Restaurant,
                User = user,
                Id = 1
            };

            Reset();

            _userRepository.Setup(x => x.GetById(1)).Returns(user);
            _currencyRepository.Setup(x => x.GetByCode("USD")).Returns(currency);
            _paymentRepository.Setup(x => x.CheckIfExists(newPayment)).Returns(true);

            PaymentServices paymentServices = CreatePaymentService();

            await Assert.ThrowsAsync<DuplicatePaymentException>(async () => await paymentServices.Create(newPayment));
        }
    }
}