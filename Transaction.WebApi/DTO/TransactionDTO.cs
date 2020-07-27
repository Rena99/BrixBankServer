using System;
using System.ComponentModel.DataAnnotations;

namespace Transaction.WebApi.DTO
{
    public class TransactionDTO
    {
        [Required]
        public Guid FromAccount { get; set; }
        [Required]
        public Guid ToAccount { get; set; }
        [Required]
        [Range(1, 1000000)]
        public int Amount { get; set; }
    }
}
