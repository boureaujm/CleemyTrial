using CleemyCommons.Model;
using System.Collections.Generic;

namespace CleemyApplication.Services
{
    public interface IPaymentServices
    {
        IEnumerable<Payment> GetPayments(int userId);
    }
}