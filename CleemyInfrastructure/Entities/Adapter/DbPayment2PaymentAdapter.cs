using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using System.Collections.Generic;

namespace CleemyInfrastructure.entities.Adapter
{
    public class DbPayment2PaymentAdapter : IEnumerableAdapter<DbPayment, Payment>
    {
        public Payment Convert(DbPayment source)
        {
            if (source == null)
                return null;

            return new Payment
            {
                Id = source.Id,
                User = new User
                {
                    Id = source.User.Id,
                    FirstName = source.User?.FirstName,
                    LastName = source.User?.LastName,
                },
                Amount = source.Amount,
                Comment = source.Comment,
                Currency = new Currency
                {
                    Code = source.Currency?.Code,
                    Label = source.Currency?.Label
                },
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