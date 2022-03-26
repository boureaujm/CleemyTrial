﻿using System;

namespace Cleemy.DTO
{
    public class PaymentDto
    {        
        public string PaymentUserLastName { get; set; }

        public string PaymentUserFirstName { get; set; }

        public DateTime? Date { get; set; }

        public string PaymentNature { get; set; }

        public double? Amount { get; set; }

        public string Currency { get; set; }

        public string Comment { get; set; }

    }
}
