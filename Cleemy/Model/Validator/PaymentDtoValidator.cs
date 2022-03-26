using Cleemy.ActionFilters;
using Cleemy.Configuration;
using Cleemy.DTO;
using CleemyCommons.Model;
using CleemyCommons.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cleemy.Model.Validator
{
    public class PaymentDtoValidator : IValidator<CreatePaymentDto>
    {
        public const string CST_MUST_BE_NOT_NULL = "Must be not null";
        public const string CST_USER_USER_ID_REQUIRED = "User Id is required";
        public const string CST_USER_LASTNAME_REQUIRED = "LastName is required";
        public const string CST_PAYEMENT_NATURE_REQUIRED = "Nature is required";
        public const string CST_PAYEMENT_NATURE_INVALID_VALUE = "Nature Invalid value";
        public const string CST_PAYEMENT_DATE_REQUIRED = "Date is required";
        public const string CST_PAYEMENT_AMOUNT_REQUIRED = "Amount is required";
        public const string CST_PAYEMENT_CURRENCY_REQUIRED = "Currency is required";
        public const string CST_PAYEMENT_COMMENT_REQUIRED = "Comment is required";

        public const string CST_PAYEMENT_DATE_NOT_IN_FUTURE = "Date can't be in future";
        public const string CST_PAYEMENT_DATE_NOT_IN_PAST_MORE_THAN_3MONTHS = "Date can't be more than 3 month in the past";

        public IEnumerable<ErrorItemDto> Validate(CreatePaymentDto paymentDto)
        {
            var errors = new List<ErrorItemDto>();

            if (paymentDto is null)
            {
                errors.Add(new ErrorItemDto
                {
                    Scope = Constants.CST_PAYMENT,
                    Reason = CST_MUST_BE_NOT_NULL
                });
            }
            else
            {
                if (paymentDto.UserId is null)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = Constants.CST_USER,
                        Reason = CST_USER_USER_ID_REQUIRED
                    });

                if (paymentDto.Date is null)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = Constants.CST_PAYMENT,
                        Reason = CST_PAYEMENT_DATE_REQUIRED
                    });

                if (paymentDto.PaymentNature is null)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = Constants.CST_PAYMENT,
                        Reason = CST_PAYEMENT_NATURE_REQUIRED
                    });
                else {
                    if (!Enum.GetNames(typeof(PaymentNatureEnum)).ToList().Contains(paymentDto.PaymentNature))
                    {
                        errors.Add(new ErrorItemDto
                        {
                            Scope = Constants.CST_PAYMENT,
                            Reason = CST_PAYEMENT_NATURE_INVALID_VALUE
                        });
                    }
                }

                if (paymentDto.Amount is null)
                        errors.Add(new ErrorItemDto
                        {
                            Scope = Constants.CST_PAYMENT,
                            Reason = CST_PAYEMENT_AMOUNT_REQUIRED
                        });

                if (paymentDto.Currency is null)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = Constants.CST_PAYMENT,
                        Reason = CST_PAYEMENT_CURRENCY_REQUIRED
                    });

                if (paymentDto.Comment is null)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = Constants.CST_PAYMENT,
                        Reason = CST_PAYEMENT_COMMENT_REQUIRED
                    });

                if (paymentDto.Date.HasValue && paymentDto.Date.Value.Date > DateTime.Now.Date)
                    errors.Add(new ErrorItemDto
                    {
                        Scope = Constants.CST_PAYMENT,
                        Reason = CST_PAYEMENT_DATE_NOT_IN_FUTURE
                    });

                if (paymentDto.Date.HasValue && paymentDto.Date.Value.Date < DateTime.Now.Date.AddMonths(-3))
                    errors.Add(new ErrorItemDto
                    {
                        Scope = Constants.CST_PAYMENT,
                        Reason = CST_PAYEMENT_DATE_NOT_IN_PAST_MORE_THAN_3MONTHS
                    });
            }

            return errors;
        }
    }
}