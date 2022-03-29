using CleemyCommons.Types;
using System;

namespace CleemyCommons.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }

        public PaymentNatureEnum PaymentNature { get; set; }

        public double Amount { get; set; }

        public Currency Currency { get; set; }

        public string Comment { get; set; }
    }
}