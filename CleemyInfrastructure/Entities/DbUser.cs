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
        [MaxLength(50)]
        public String LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public String FirstName { get; set; }

        public string AuthorizedCurrencyCode { get; set; }

        [ForeignKey("AuthorizedCurrencyCode")]
        [Required]
        public virtual DbCurrency AuthorizedCurrency { get; set; }
    }
}