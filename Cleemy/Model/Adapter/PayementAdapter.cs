using Cleemy.DTO;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using System.Collections.Generic;

namespace Cleemy.Model.Adapter
{
    public class PayementAdapter : IEnumerableAdapter<Payment, PaymentDto>
    {
        public PaymentDto Convert(Payment source)
        {
            return new PaymentDto
            {
                UserId = source.UserId,
                Amount = source.Amount,
                Comment = source.Comment,
                Currency = source.Currency,
                Date = source.Date,
                PaymentNature = source.PaymentNature
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
