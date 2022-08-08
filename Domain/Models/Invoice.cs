using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(200)]
        public string CustomerName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime IssuedDate { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
    }
}
