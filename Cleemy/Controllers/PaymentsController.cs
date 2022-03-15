using Cleemy.DTO;
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

        public PaymentsController(ILogger<PaymentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PaymentDto> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new PaymentDto
            {
                Amount = rng.NextDouble(),
                Date = DateTime.Now
            })
            .ToArray();
        }
    }
}
