using Cleemy.DTO;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using System.Collections.Generic;

namespace Cleemy.Model.Adapter
{
    public class PaymentToPaymentDtoAdapter : IEnumerableAdapter<Payment, PaymentDto>
    {
        public PaymentDto Convert(Payment source)
        {
            if (source == null)
                return null;

            return new PaymentDto
            {
                Id = source.Id,
                User = new UserDto
                {
                    Id = source.User.Id,
                    FirstName = source.User.FirstName,
                    LastName = source.User.LastName,
                    AuthorizedCurrency = source.Currency.Code
                },

                Amount = source.Amount,
                Comment = source.Comment,

                Currency = source.Currency.Label,

                Date = source.Date,
                PaymentNature = source.PaymentNature.ToString()
            };
        }

        public IEnumerable<PaymentDto> Convert(IList<Payment> list)
        {
            foreach (var item in list)
            {
                yield return Convert(item);
            }
        }
    }
}