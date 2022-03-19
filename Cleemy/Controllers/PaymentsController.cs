using Cleemy.DTO;
using CleemyApplication.Services;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentServices _paymentServices;
        private readonly IEnumerableAdapter<Payment, PaymentDto> _adapter;

        public PaymentsController(ILogger<PaymentsController> logger,
            IPaymentServices paymentServices,
            IEnumerableAdapter<Payment, PaymentDto> adapter)
        {
            _logger = logger;
            _paymentServices = paymentServices;
            _adapter = adapter;
        }

        [HttpGet]
        public IEnumerable<PaymentDto> Get(int userId)
        {
            var payments = _paymentServices.GetPayments(userId).ToList();

            var paymentsDto = _adapter.Convert(payments);

            return paymentsDto;
        }
    }
}
