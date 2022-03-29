using Newtonsoft.Json;
using System;

namespace Cleemy.DTO
{
    public class UserDto
    {
        [JsonProperty(Order = 7)]
        public int Id { get; set; }

        public String LastName { get; set; }

        public String FirstName { get; set; }

        public string AuthorizedCurrency { get; set; }
    }
}