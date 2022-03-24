using CleemyCommons.Types;
using System;

namespace Cleemy.DTO
{
    public class PaymentDto
    {
        public int Id { get; set; }
        
        public string PaymentUserFirstName { get; set; }
        public string PaymentUserLastName { get; set; }

        public DateTime? Date { get; set; }

        public PaymentNatureEnum? PaymentNature { get; set; }

        public double? Amount { get; set; }

        public string Currency { get; set; }

        public string Comment { get; set; }

    }
}
