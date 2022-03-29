using Newtonsoft.Json;
using System;

namespace Cleemy.DTO
{
    public class PaymentDto
    {
        [JsonProperty(Order = 1)]
        public int Id { get; set; }

        [JsonProperty(Order = 2)]
        public double? Amount { get; set; }

        [JsonProperty(Order = 3)]
        public UserDto User { get; set; }

        [JsonProperty(Order = 4)]
        public DateTime? Date { get; set; }

        [JsonProperty(Order = 5)]
        public string PaymentNature { get; set; }

        [JsonProperty(Order = 6)]
        public string Currency { get; set; }

        [JsonProperty(Order = 7)]
        public string Comment { get; set; }
    }
}