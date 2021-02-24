using PaymentWebApp.Core.Models;

namespace PaymentWebApp.Core.Interfaces
{
    public interface IPremiumPaymentService
    {
        public (string, string) ProcessPremiumPayment(PaymentDetail paymentDetail);
    }
}
