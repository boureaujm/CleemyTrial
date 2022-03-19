using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleemyInfrastructure.entities
{
    [Table("Users")]
    public class DbUser
    {
        public int Id { get; set; }

        [Required]
        public String LastName { get; set; }

        [Required]
        public String FirstName { get; set; }

        public string AuthorizedCurrencyCode { get; set; }

        [ForeignKey("AuthorizedCurrencyCode")]
        [Required]
        public virtual DbCurrency AuthorizedCurrency { get; set; }
    }
}
