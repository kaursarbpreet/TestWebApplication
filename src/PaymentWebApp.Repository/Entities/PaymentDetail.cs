using PaymentWebApp.Repository.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentWebApp.Repository.Entities
{
    [Table("PaymentDetail")]
    public class PaymentDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PaymentDetailId")]
        public int PaymentDetailId { get; set; }
        [CreditCard,Required]
        public string CreditCardNumber { get; set; }
        [MaxLength(100), Required]
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        [MaxLength(3)]
        public string SecurityCode { get; set; }
        [DecimalPrecision(20, 10),Required]
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
    }
}
