using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Enums;

namespace Transaction.Domain.Entities
{
    public class Transaction
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid sourceAccountId { get; set; }

        [Required]
        public Guid targetAccountId { get; set; }

        [Required]
        [MaxLength(10)]
        public int transferTypeId { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal amount { get; set; }

        [Required]
        [MaxLength(10)]
        public string? status { get; set; }

        [Required]
        public DateTime createAt { get; set; }       
    }
}
