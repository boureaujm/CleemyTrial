using Cleemy.DTO;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using System.Collections.Generic;

namespace Cleemy.Model.Adapter
{
    public class PaymentToDtoAdapter : IEnumerableAdapter<Payment, PaymentDto>
    {
        public PaymentDto Convert(Payment source)
        {
            return new PaymentDto
            {
                PaymentUserFirstName = source.User.FirstName,
                PaymentUserLastName = source.User.LastName,

                Amount = source.Amount,
                Comment = source.Comment,
                
                Currency = source.Currency.Label,

                Date = source.Date,
                PaymentNature = "test"
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
