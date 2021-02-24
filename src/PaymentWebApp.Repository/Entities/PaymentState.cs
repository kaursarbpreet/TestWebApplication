using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentWebApp.Repository.Entities
{
    [Table("PaymentState")]
    public class PaymentState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PaymentStateId")]
        public int PaymentStateId { get; set; }
        [Required]
        [ForeignKey("PaymentDetail")]
        public int PaymentDetailId { get; set; }
        public PaymentDetail PaymentDetail { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
