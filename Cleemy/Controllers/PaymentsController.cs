using Cleemy.DTO;
using CleemyApplication.Services;
using CleemyCommons.Interfaces;
using CleemyCommons.Model;
using CleemyCommons.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cleemy.Controllers
{
    /// <summary>
    /// Payments controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : CleemyBaseController
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentServices _paymentServices;
        private readonly IEnumerableAdapter<Payment, PaymentDto> _adapter;

        const string Default_Payment_Sorter = PaymentConstants.CST_DATE + "." + CommonsConstants.CST_DESCENDING;

        public PaymentsController(ILogger<PaymentsController> logger,
            IPaymentServices paymentServices,
            IEnumerableAdapter<Payment, PaymentDto> adapter)
        {
            _logger = logger;
            _paymentServices = paymentServices;
            _adapter = adapter;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromRoute]int userId, [FromQuery]string sortBy = Default_Payment_Sorter)
        {
            var sortWrapper = new SortWrapper
            {
                Field = PaymentConstants.CST_AMOUNT,
                Direction = CommonsConstants.CST_ASCENDING
            };

            var payments = _paymentServices.GetByUserId(userId, sortWrapper);

            var paymentsDto = _adapter.Convert(payments.ToList());

            return Ok<IEnumerable<PaymentDto>>(paymentsDto);
        }
    }
}
