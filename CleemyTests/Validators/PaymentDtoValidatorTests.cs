using Cleemy.DTO;
using Cleemy.Model.Validator;
using System.ComponentModel;
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
        [Trait("Payment","Validation")]
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

    }
}