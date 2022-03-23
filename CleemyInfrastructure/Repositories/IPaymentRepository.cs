using CleemyCommons.Model;
using CleemyInfrastructure.entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleemyInfrastructure.Repositories
{
    public interface IPaymentRepository
    {
        bool CheckIfExists(Payment newPayment);
        Task<DbPayment> CreateAsync(Payment payment);
        IEnumerable<DbPayment> GetByUser(int userId, SortWrapper sortWrapper);
    }
}