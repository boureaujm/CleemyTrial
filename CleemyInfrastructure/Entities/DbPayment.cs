using CleemyCommons.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleemyInfrastructure.entities
{
    [Table("Payments")]
    public class DbPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual DbUser User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public PaymentNatureEnum PaymentNature { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public virtual DbCurrency Currency { get; set; }

        [Required]
        [MaxLength(250)]
        public string Comment { get; set; }

    }
}
