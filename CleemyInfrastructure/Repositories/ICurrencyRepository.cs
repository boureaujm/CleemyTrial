using CleemyCommons.Model;

namespace CleemyInfrastructure.Repositories
{
    public interface ICurrencyRepository
    {
        Currency GetByCode(string currencyCode);
    }
}