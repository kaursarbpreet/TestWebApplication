using System.Text.Json.Serialization;

namespace PaymentWebApp.Core.Models
{
    public class PaymentDetail 
    {
        [JsonIgnore]
        public int PaymentDetailId { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
    }
}
