using Cleemy.DTO;
using Cleemy.Model.Validator;
using System.Linq;
using Xunit;

namespace CleemyTests
{
    public class SortWrapperDtoValidatorTests
    {
        private readonly SortWrapperDtoValidator _validator;

        public SortWrapperDtoValidatorTests()
        {
            _validator = new SortWrapperDtoValidator();
        }

        [Fact]
        [Trait("PaymentList.Sort", "Validation")]
        public void SortWrapperDto_FieldNotNull_GenerateErrors_Ok()
        {
            var sortWrapper = new SortWrapperDto
            {
                Direction = null,
                Field = null,
            };

            var errors = _validator.Validate(sortWrapper);

            Assert.True(errors.Where(e => e.Reason == SortWrapperDtoValidator.CST_SORT_FIELD_REQUIRED).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == SortWrapperDtoValidator.CST_SORT_DIRECTION_REQUIRED).Count() == 1);
        }

        [Fact]
        [Trait("PaymentList.Sort", "Validation")]
        public void GetPayments_InvalidNature_GenerateErrors_Ok()
        {
            var sortWrapper = new SortWrapperDto
            {
                Direction = "test",
                Field = "test",
            };

            var errors = _validator.Validate(sortWrapper);

            Assert.True(errors.Where(e => e.Reason == SortWrapperDtoValidator.CST_SORT_INVALID_FIELD_NAME).Count() == 1);
            Assert.True(errors.Where(e => e.Reason == SortWrapperDtoValidator.CST_SORT_INVALID_DIRECTION_NAME).Count() == 1);
        }

        [Fact]
        [Trait("PaymentList.Sort", "Validation")]
        public void GetPayments_PaymentNull_GenerateError_Ok()
        {
            SortWrapperDto sortWrapper = null;

            var errors = _validator.Validate(sortWrapper);

            Assert.True(errors.Where(e => e.Reason == SortWrapperDtoValidator.CST_MUST_BE_NOT_NULL).Count() == 1);
        }

    }
}