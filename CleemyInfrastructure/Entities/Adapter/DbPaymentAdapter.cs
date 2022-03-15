using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using System.Collections.Generic;

namespace CleemyInfrastructure.entities.Adapter
{
    public class DbPaymentAdapter : IEnumerableAdapter<DbPayment, Payment>
    {
        public Payment Convert(DbPayment source)
        {
            return new Payment
            {
                UserId = source.UserId,
                Amount = source.Amount,
                Comment = source.Comment,
                Currency = source.Currency,
                Date = source.Date,
                PaymentNature = source.PaymentNature
            };
        }

        public IEnumerable<Payment> Convert(IList<DbPayment> list)
        {
            foreach (var item in list)
            {
                yield return Convert(item);
            }
        }
    }
}
