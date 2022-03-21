using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using System.Collections.Generic;

namespace CleemyInfrastructure.entities.Adapter
{
    public class DbCurrency2CurrencyAdapter : IEnumerableAdapter<DbCurrency, Currency>
    {
        public Currency Convert(DbCurrency source)
        {
            return new Currency
            {
                Code = source.Code,
                Label = source.Label
            };
        }

        public IEnumerable<Currency> Convert(IList<DbCurrency> list)
        {
            foreach (var item in list)
            {
                yield return Convert(item);
            }
        }
    }
}
