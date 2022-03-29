using Cleemy.DTO;
using Cleemy.Model.Validator;
using CleemyCommons.Types;
using System.Linq;
using Xunit;

namespace CleemyTests
{
    public class PaymentDtoValidatorTests
    {
        private readonly PaymentDtoValidator _validator;

        public PaymentDtoValidatorTests()
        {
            _validator = new PaymentDtoValidator();
        }

        [Fact]
        [Trait("Payment", "Validation")]
        public void PaymentDto_FieldNotNull_GenerateErrors_Ok()
        {
            var payment = new CreatePaymentDto
            {
                Amount = null,
                Comment = null,
                Currency = null,
                Date = null,
                UserId = null,
                PaymentNature = null
            };

            var errors = _validator.Validate(payment);

            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_AMOUNT_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_COMMENT_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_CURRENCY_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_DATE_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_USER_USER_ID_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_NATURE_REQUIRED).Count() == 1);
        }

        [Fact]
        [Trait("Payment", "Validation")]
        public void PaymentDto_InvalidNature_GenerateErrors_Ok()
        {
            var payment = new CreatePaymentDto
            {
                Amount = null,
                Comment = null,
                Currency = null,
                Date = null,
                PaymentNature = "test"
            };

            var errors = _validator.Validate(payment);

            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_NATURE_INVALID_VALUE).Count() == 1);
        }

        [Fact]
        [Trait("Payment", "Validation")]
        public void PaymentDto_PaymentNull_GenerateError_Ok()
        {
            CreatePaymentDto payment = null;

            var errors = _validator.Validate(payment);

            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_MUST_BE_NOT_NULL).Count() == 1);
        }

        [Fact]
        [Trait("Payment", "Validation")]
        public void PaymentDto_Payment_Not_In_Future_GenerateError_Ok()
        {
            var newPaymentDto = new CreatePaymentDto
            {
                Amount = 100,
                Comment = "test",
                Currency = "USD",
                Date = System.DateTime.Now.AddDays(1),
                PaymentNature = PaymentNatureEnum.Restaurant.ToString(),
                UserId = 1
            };

            var errors = _validator.Validate(newPaymentDto);

            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_DATE_NOT_IN_FUTURE).Count() == 1);
        }

        [Fact]
        [Trait("Payment", "Validation")]
        public void PaymentDto_Payment_No_More_Three_Month_In_Past_GenerateError_Ok()
        {
            var newPaymentDto = new CreatePaymentDto
            {
                Amount = 100,
                Comment = "test",
                Currency = "USD",
                Date = System.DateTime.Now.AddMonths(-4),
                PaymentNature = PaymentNatureEnum.Restaurant.ToString(),
                UserId = 1
            };

            var errors = _validator.Validate(newPaymentDto);

            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_DATE_NOT_IN_PAST_MORE_THAN_3MONTHS).Count() == 1);
        }
    }
}