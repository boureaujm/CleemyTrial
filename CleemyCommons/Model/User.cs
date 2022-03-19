using System;

namespace CleemyCommons.Model
{
    public class User
    {
        public int Id { get; set; }

        public String LastName { get; set; }

        public String FirstName { get; set; }

        public Currency AuthorizedCurrency { get; set; }
    }
}
