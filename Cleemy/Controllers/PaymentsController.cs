using Cleemy.ActionFilters;
using Cleemy.Configuration;
using Cleemy.DTO;
using CleemyApplication.Services;
using CleemyCommons.Exceptions;
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
    /// <summary>
    /// Payments controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : CleemyBaseController
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentService _paymentServices;
        private readonly IEnumerableAdapter<CreatePaymentDto, Payment> _paymentDtoTopaymentAdapter;
        private readonly IEnumerableAdapter<Payment, PaymentDto> _paymentToPaymentDtoAdapter;
        private readonly IAdapter<SortWrapperDto, SortWrapper> _sortWrapperAdapter;

        public PaymentsController(ILogger<PaymentsController> logger,
            IPaymentService paymentServices,
            IEnumerableAdapter<CreatePaymentDto, Payment> paymentDtoTopaymentAdapter,
            IEnumerableAdapter<Payment, PaymentDto> paymentToPaymentDtoAdapter,
            IAdapter<SortWrapperDto, SortWrapper> sortWrapperAdapter
            )
        {
            _logger = logger;
            _paymentServices = paymentServices;
            _paymentDtoTopaymentAdapter = paymentDtoTopaymentAdapter;
            _paymentToPaymentDtoAdapter = paymentToPaymentDtoAdapter;
            _sortWrapperAdapter = sortWrapperAdapter;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateModelStateAttribute<SortWrapperDto>))]
        public async Task<ActionResult> GetAsync([FromQuery] int userId, [FromQuery] SortWrapperDto sortWrapperDto)
        {
            try
            {
                _logger.LogDebug($"Get Payments : userId = {userId}", sortWrapperDto);

                var sortWrapper = _sortWrapperAdapter.Convert(sortWrapperDto);

                var payments = _paymentServices.GetByUserId(userId, sortWrapper);
                if (payments is null || payments.Count() == 0)
                    return NotFound<string>(Constants.CST_MESSAGE_NOT_FOUND);

                var paymentsDto = _paymentToPaymentDtoAdapter.Convert(payments.ToList());

                _logger.LogDebug($"Get Payments return : ", paymentsDto);

                return Ok<IEnumerable<PaymentDto>>(paymentsDto);
            }
            catch (Exception err)
            {
                return BadRequest<ErrorsDto>(err);
                _logger.LogError(Constants.CST_MESSAGE_INTERNAL_ERROR, err);
                throw;
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateModelStateAttribute<CreatePaymentDto>))]
        public async Task<ActionResult> AddPaymentAsync([FromBody] CreatePaymentDto paymentDto)
        {
            try
            {
                _logger.LogDebug($"Add Payment", paymentDto);

                var payment = _paymentDtoTopaymentAdapter.Convert(paymentDto);

                var newPayment = await _paymentServices.Create(payment);

                if (newPayment is null)
                    return NotFound<string>(Constants.CST_MESSAGE_NOT_FOUND);

                var newPaymentDto = _paymentToPaymentDtoAdapter.Convert(newPayment);

                _logger.LogDebug($"Add Payment return : ", newPaymentDto);

                return Ok<PaymentDto>(newPaymentDto);
            }
            catch (UserNotExistException userNotExistEx)
            {
                return BadRequest<ErrorsDto>(userNotExistEx);
                throw;
            }
            catch (CurrencyNotExistException currencyNotExistEx)
            {
                return BadRequest<ErrorsDto>(currencyNotExistEx);
                throw;
            }
            catch (CurrencyIncoherenceException currencyIncoherenceEx)
            {
                return BadRequest<ErrorsDto>(currencyIncoherenceEx);
                throw;
            }
            catch (DuplicatePaymentException duplicatePaymentEx)
            {
                return Conflict<ErrorsDto>(duplicatePaymentEx);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.CST_MESSAGE_INTERNAL_ERROR, ex);
                return InternalError<string>(Constants.CST_MESSAGE_INTERNAL_ERROR);
                throw;
            }
        }
    }
}