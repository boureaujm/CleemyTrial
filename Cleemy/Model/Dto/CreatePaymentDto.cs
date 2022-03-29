using System;

namespace Cleemy.DTO
{
    public class CreatePaymentDto
    {
        public int? UserId { get; set; }

        public DateTime? Date { get; set; }

        public string PaymentNature { get; set; }

        public double? Amount { get; set; }

        public string Currency { get; set; }

        public string Comment { get; set; }
    }
}