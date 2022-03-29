using CleemyCommons.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleemyApplication.Services
{
    public interface IPaymentServices
    {
        Task<Payment> Create(Payment newPayment);

        IEnumerable<Payment> GetByUserId(int userId, SortWrapper sortWrapper);
    }
}