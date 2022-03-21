using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyCommons.Tools;
using CleemyInfrastructure.entities;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CleemyInfrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IEnumerableAdapter<DbCurrency, Currency> _dbCurrencyToCurrencyAdapter;
        private ApplicationContext _context;
        private readonly ILogger<CurrencyRepository> _logger;

        public CurrencyRepository(
            IEnumerableAdapter<DbCurrency, Currency> dbCurrencyToCurrencyAdapter,
            ApplicationContext context,
            ILogger<CurrencyRepository> logger
            )
        {
            _dbCurrencyToCurrencyAdapter = dbCurrencyToCurrencyAdapter;
            _context = context;
            _logger = logger;
        }

        public Currency GetByCode(string currencyCode)
        {

            Guard.IsNotNull(currencyCode, "currencyCode nust have a value");

            try
            {
                var dbCurrency = _context.Currencies.FirstOrDefault(p => p.Code == currencyCode);
                var currency = _dbCurrencyToCurrencyAdapter.Convert(dbCurrency);
                return currency;
            }
            catch (Exception err)
            {
                _logger.LogError("Get Currency", err);
                return null;
            }

        }
    }
}
