using Cleemy.DTO;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyCommons.Types;
using System;
using System.Collections.Generic;

namespace Cleemy.Model.Adapter
{
    public class CreatePaymentDtoToPaymentAdapter : IEnumerableAdapter<CreatePaymentDto, Payment>
    {
        public Payment Convert(CreatePaymentDto source)
        {
            if (source == null)
                return null;

            var paymentNature = Enum.TryParse<PaymentNatureEnum>(source.PaymentNature, out var nature);

            return new Payment
            {
                User = new User
                {
                    Id = source.UserId.Value
                },

                Amount = source.Amount.Value,
                Comment = source.Comment,

                Currency = new Currency { Code = source.Currency },

                Date = source.Date.Value,
                PaymentNature = nature
            };
        }

        public IEnumerable<Payment> Convert(IList<CreatePaymentDto> list)
        {
            foreach (var item in list)
            {
                yield return Convert(item);
            }
        }
    }
}