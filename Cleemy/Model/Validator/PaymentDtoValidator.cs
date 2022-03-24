using Cleemy.ActionFilters;
using Cleemy.DTO;
using CleemyCommons.Model;
using System;
using System.Collections.Generic;

namespace Cleemy.Model.Validator
{
    public class PaymentDtoValidator : IValidator<PaymentDto>
    {
        public IEnumerable<ErrorItemDto> Validate(PaymentDto paymentDto)
        {
            var errors = new List<ErrorItemDto>();

            if (paymentDto is null)
                errors.Add(new ErrorItemDto { 
                    Scope = "Payment",
                    Reason = "Must be not null"
                });

            if (paymentDto.PaymentUserFirstName is null)
                errors.Add(new ErrorItemDto
                {
                    Scope = "User",
                    Reason = "FirstName is required"
                });

            if (paymentDto.PaymentUserLastName is null)
                errors.Add(new ErrorItemDto
                {
                    Scope = "User",
                    Reason = "LastName is required"
                });

            if (paymentDto.Date is null)
                errors.Add(new ErrorItemDto
                {
                    Scope = "Date",
                    Reason = "Date is required"
                });

            if (paymentDto.Amount is null)
                errors.Add(new ErrorItemDto
                {
                    Scope = "Amount",
                    Reason = "Amount is required"
                });

            if (paymentDto.Currency is null)
                errors.Add(new ErrorItemDto
                {
                    Scope = "Currency",
                    Reason = "Currency is required"
                });

            if (paymentDto.Comment is null)
                errors.Add(new ErrorItemDto
                {
                    Scope = "Comment",
                    Reason = "Comment is required"
                });

            if (paymentDto.Date.HasValue && paymentDto.Date.Value.Date > DateTime.Now.Date)
                errors.Add(new ErrorItemDto
                {
                    Scope = "Date",
                    Reason = "Date can't be in future"
                });

            if (paymentDto.Date.HasValue && paymentDto.Date.Value.Date < DateTime.Now.Date.AddMonths(-3))
                errors.Add(new ErrorItemDto
                {
                    Scope = "Date",
                    Reason = "Date can't be more than 3 month in the past"
                });

            return errors;
        }
    }
}
