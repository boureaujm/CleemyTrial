using Cleemy.Controllers;
using Cleemy.DTO;
using Cleemy.Model.Adapter;
using CleemyApplication.Services;
using CleemyCommons.Exceptions;
using CleemyCommons.Model;
using CleemyCommons.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CleemyTests
{
    public class CreatePaymentsControllerTests
    {
        private readonly Mock<IPaymentService> _paymentService;
        private readonly Mock<ILogger<PaymentsController>> _logger;
        private readonly PaymentToPaymentDtoAdapter _paymentToPaymentDtoAdapter;
        private readonly CreatePaymentDtoToPaymentAdapter _paymentDtoToPaymentAdapter;
        private readonly SortWrapperDtoToSortWrapperAdapter _sortAdapter;

        public CreatePaymentsControllerTests()
        {
            _paymentService = new Mock<IPaymentService>();
            _logger = new Mock<ILogger<PaymentsController>>();
            _paymentToPaymentDtoAdapter = new PaymentToPaymentDtoAdapter();
            _paymentDtoToPaymentAdapter = new CreatePaymentDtoToPaymentAdapter();
            _sortAdapter = new SortWrapperDtoToSortWrapperAdapter();
        }

        private PaymentsController CreateController()
        {
            return new PaymentsController(_logger.Object, _paymentService.Object,
                _paymentDtoToPaymentAdapter, _paymentToPaymentDtoAdapter, _sortAdapter);
        }

        [Fact]
        [Trait("PaymentCreate", "ServiceExceptions")]
        public async Task GetPayments_GetPayment_UserNotExistException_Return_Not_Found()
        {
            var newPaymentDto = new CreatePaymentDto
            {
                Amount = 100,
                Comment = "test",
                Currency = "USD",
                Date = System.DateTime.Now,
                PaymentNature = PaymentNatureEnum.Restaurant.ToString(),
                UserId = 1
            };

            _paymentService.Setup(x => x.Create(It.IsAny<Payment>())).Throws(new UserNotExistException());

            PaymentsController controller = CreateController();

            ActionResult<IEnumerable<CreatePaymentDto>> result = await controller.AddPaymentAsync(newPaymentDto);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        [Trait("PaymentCreate", "ServiceExceptions")]
        public async Task GetPayments_GetPayment_CurrencyNotExistException_Return_Not_Found()
        {
            var newPaymentDto = new CreatePaymentDto
            {
                Amount = 100,
                Comment = "test",
                Currency = "USD",
                Date = System.DateTime.Now,
                PaymentNature = PaymentNatureEnum.Restaurant.ToString(),
                UserId = 1
            };

            _paymentService.Setup(x => x.Create(It.IsAny<Payment>())).Throws(new CurrencyNotExistException());

            PaymentsController controller = CreateController();

            ActionResult<IEnumerable<CreatePaymentDto>> result = await controller.AddPaymentAsync(newPaymentDto);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        [Trait("PaymentCreate", "ServiceExceptions")]
        public async Task GetPayments_GetPayment_CurrencyIncoherenceException_Return_Not_Found()
        {
            var newPaymentDto = new CreatePaymentDto
            {
                Amount = 100,
                Comment = "test",
                Currency = "USD",
                Date = System.DateTime.Now,
                PaymentNature = PaymentNatureEnum.Restaurant.ToString(),
                UserId = 1
            };

            _paymentService.Setup(x => x.Create(It.IsAny<Payment>())).Throws(new CurrencyIncoherenceException());

            PaymentsController controller = CreateController();

            ActionResult<IEnumerable<CreatePaymentDto>> result = await controller.AddPaymentAsync(newPaymentDto);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        [Trait("PaymentCreate", "ServiceExceptions")]
        public async Task GetPayments_GetPayment_DuplicatePaymentException_Return_Not_Found()
        {
            var newPaymentDto = new CreatePaymentDto
            {
                Amount = 100,
                Comment = "test",
                Currency = "USD",
                Date = System.DateTime.Now,
                PaymentNature = PaymentNatureEnum.Restaurant.ToString(),
                UserId = 1
            };

            _paymentService.Setup(x => x.Create(It.IsAny<Payment>())).Throws(new DuplicatePaymentException());

            PaymentsController controller = CreateController();

            ActionResult<IEnumerable<CreatePaymentDto>> result = await controller.AddPaymentAsync(newPaymentDto);

            Assert.IsType<ConflictObjectResult>(result.Result);
        }

        [Fact]
        [Trait("PaymentCreate", "Service")]
        public async Task GetPayments_GetPayment_Create_Return_Ok()
        {
            var newPaymentDto = new CreatePaymentDto
            {
                Amount = 100,
                Comment = "test",
                Currency = "USD",
                Date = System.DateTime.Now,
                PaymentNature = PaymentNatureEnum.Restaurant.ToString(),
                UserId = 1
            };

            var currency = new Currency { Code = "USD", Label = "US Dollar" };

            var newPayment = new Payment
            {
                Amount = 100,
                Comment = "test",
                Currency = currency,
                Date = System.DateTime.Now,
                PaymentNature = PaymentNatureEnum.Restaurant,
                User = new User
                {
                    Id = 1,
                    AuthorizedCurrency = currency,
                    FirstName = "test",
                    LastName = "test"
                }
            };

            _paymentService.Setup(x => x.Create(It.IsAny<Payment>())).Returns(Task.FromResult(newPayment));

            PaymentsController controller = CreateController();

            ActionResult<IEnumerable<CreatePaymentDto>> result = await controller.AddPaymentAsync(newPaymentDto);

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}