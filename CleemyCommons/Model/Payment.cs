using CleemyCommons.Types;
using System;

namespace CleemyCommons.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public PaymentNatureEnum PaymentNature { get; set; }

        public double Amount { get; set; }

        public CurrencyEnum Currency { get; set; }

        public string Comment { get; set; }

    }
}
