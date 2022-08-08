using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class HealthyAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CustomerId { get; set; }
        [Required]
        [StringLength(200)]
        public string CustomerName { get; set; } = "Customer name";
        public bool? IsHealthy { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
