using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using System.Collections.Generic;

namespace CleemyInfrastructure.entities.Adapter
{
    public class Payment2DbPaymentAdapter : IEnumerableAdapter<Payment, DbPayment>
    {
        public DbPayment Convert(Payment source)
        {
            return new DbPayment
            {
                Amount = source.Amount,
                Comment = source.Comment,
                Date = source.Date,
                PaymentNature = source.PaymentNature
            };
        }

        public IEnumerable<DbPayment> Convert(IList<Payment> list)
        {
            foreach (var item in list)
            {
                yield return Convert(item);
            }
        }
    }
}
