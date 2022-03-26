using Cleemy.ActionFilters;
using Cleemy.DTO;
using CleemyCommons.Model;
using CleemyCommons.Types;
using System.Collections.Generic;

namespace Cleemy.Model.Validator
{
    public class SortWrapperDtoValidator : IValidator<SortWrapperDto>
    {
        private const string CST_SORT = "Sort";
        
        public const string CST_MUST_BE_NOT_NULL = "Must be not null";

        public const string CST_SORT_FIELD_REQUIRED = "Field is required";
        public const string CST_SORT_DIRECTION_REQUIRED = "Direction is required";
        public const string CST_SORT_INVALID_FIELD_NAME = "Invalid Field Name";
        public const string CST_SORT_INVALID_DIRECTION_NAME = "Invalid Direction Name";

        public IEnumerable<ErrorItemDto> Validate(SortWrapperDto sortWrapperDto)
        {
            var errors = new List<ErrorItemDto>();

            if (sortWrapperDto is null) {
                errors.Add(new ErrorItemDto
                {
                    Scope = CST_SORT,
                    Reason = CST_MUST_BE_NOT_NULL
                });
            }
            else
            {
                if (sortWrapperDto.Field == null)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = CST_SORT,
                        Reason = CST_SORT_FIELD_REQUIRED
                    });

                if (sortWrapperDto.Direction == null)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = CST_SORT,
                        Reason = CST_SORT_DIRECTION_REQUIRED
                    });

                if (sortWrapperDto.Field != PaymentConstants.CST_AMOUNT && sortWrapperDto.Field != PaymentConstants.CST_DATE)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = CST_SORT,
                        Reason = CST_SORT_INVALID_FIELD_NAME
                    });

                if (sortWrapperDto.Direction != CommonsConstants.CST_DESCENDING && sortWrapperDto.Direction != CommonsConstants.CST_ASCENDING)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = CST_SORT,
                        Reason = CST_SORT_INVALID_DIRECTION_NAME
                    });
            }

            return errors;
        }
    }
}