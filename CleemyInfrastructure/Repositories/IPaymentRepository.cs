using CleemyCommons.Model;
using System.Collections.Generic;

namespace CleemyInfrastructure.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetPayments(int userId);
    }
}