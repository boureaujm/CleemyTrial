using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleemyInfrastructure.entities
{
    [Table("Currencies")]
    public class DbCurrency
    {
        [Key]
        public string Code { get; set; }

        [Required]
        public string Label { get; set; }
    }
}
