using Cleemy.DTO;
using Cleemy.Model.Validator;
using System.Linq;
using Xunit;

namespace CleemyTests
{
    public class PaymentValidatorTests
    {
        private readonly PaymentDtoValidator _validator;

        public PaymentValidatorTests()
        {
            _validator = new PaymentDtoValidator();
        }

        [Fact]
        public void GetPayments_FieldNotNull_GenerateErrors_Ok()
        {
            var payment = new PaymentDto
            {
                Amount = null,
                Comment = null,
                Currency = null,
                Date = null,    
                PaymentUserFirstName = null,
                PaymentUserLastName = null,
                PaymentNature = null
            };

            var errors = _validator.Validate(payment);

            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_AMOUNT_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_COMMENT_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_CURRENCY_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_DATE_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_USER_FIRSTNAME_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_USER_LASTNAME_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_NATURE_REQUIRED).Count() == 1);
        }

        [Fact]
        public void GetPayments_InvalidNature_GenerateErrors_Ok()
        {
            var payment = new PaymentDto
            {
                Amount = null,
                Comment = null,
                Currency = null,
                Date = null,
                PaymentUserFirstName = null,
                PaymentUserLastName = null,
                PaymentNature = "test"
            };

            var errors = _validator.Validate(payment);

            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_PAYEMENT_NATURE_INVALID_VALUE).Count() == 1);
        }

        [Fact]
        public void GetPayments_PaymentNull_GenerateError_Ok()
        {
            PaymentDto payment = null;

            var errors = _validator.Validate(payment);

            Assert.True(errors.Where(e => e.Reason == PaymentDtoValidator.CST_MUST_BE_NOT_NULL).Count() == 1);
        }

    }
}