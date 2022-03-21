using CleemyCommons.Model;
using CleemyInfrastructure.entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleemyInfrastructure.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateAsync(Payment payment);
        IEnumerable<Payment> GetByUser(int userId);
    }
}