using Cleemy.Controllers;
using Cleemy.DTO;
using Cleemy.Model.Adapter;
using Cleemy.Model.Validator;
using CleemyApplication.Services;
using CleemyCommons.Model;
using CleemyCommons.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CleemyTests
{
    public class PaymentsControllerTests
    {
        [Fact]
        [Trait("PaymentList", "Validation")]
        public async Task GetPayments_GetPayment_UnknowUser_Return_Not_Found()
        {
            var paymentService = new Mock<IPaymentServices>();
            var logger = new Mock<ILogger<PaymentsController>>();
            var paymentToPaymentDtoAdapter  = new PaymentToPaymentDtoAdapter();
            var paymentDtoToPaymentAdapter = new CreatePaymentDtoToPaymentAdapter();
            var sortAdapter = new SortWrapperDtoToSortWrapperAdapter();
            var sortWrapper = new SortWrapper
            {
                Field = PaymentConstants.CST_AMOUNT,
                Direction = CommonsConstants.CST_ASCENDING
            };

            var sortWrapperDto = new SortWrapperDto
            {
                Field = PaymentConstants.CST_AMOUNT,
                Direction = CommonsConstants.CST_ASCENDING
            };

            paymentService.Setup(x => x.GetByUserId(It.IsAny<int>(), sortWrapper)).Returns(new List<Payment>());

            var controller = new PaymentsController(logger.Object, paymentService.Object, 
                paymentDtoToPaymentAdapter, paymentToPaymentDtoAdapter, sortAdapter);

            ActionResult<IEnumerable<CreatePaymentDto>> result = await controller.GetAsync(9, sortWrapperDto);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        [Trait("PaymentList", "Validation")]
        public async Task GetPayments_GetPayment_Return_Ok()
        {
            var paymentService = new Mock<IPaymentServices>();
            var logger = new Mock<ILogger<PaymentsController>>();
            var paymentToPaymentDtoAdapter = new PaymentToPaymentDtoAdapter();
            var paymentDtoToPaymentAdapter = new CreatePaymentDtoToPaymentAdapter();
            var sortAdapter = new SortWrapperDtoToSortWrapperAdapter();

            var sortWrapper = new SortWrapper
            {
                Field = PaymentConstants.CST_AMOUNT,
                Direction = CommonsConstants.CST_ASCENDING
            };

            var sortWrapperDto = new SortWrapperDto
            {
                Field = PaymentConstants.CST_AMOUNT,
                Direction = CommonsConstants.CST_ASCENDING
            };

            paymentService.Setup(service => service.GetByUserId(It.IsAny<int>(), It.IsAny<SortWrapper>())).Returns((new List<Payment>{ new Payment() }).AsEnumerable());

            var controller = new PaymentsController(logger.Object, paymentService.Object,
              paymentDtoToPaymentAdapter, paymentToPaymentDtoAdapter, sortAdapter);

            ActionResult<IEnumerable<CreatePaymentDto>> result = await controller.GetAsync(9, sortWrapperDto);

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}