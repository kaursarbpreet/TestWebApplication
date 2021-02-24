using PaymentWebApp.Core.Models;

namespace PaymentWebApp.Core.Interfaces
{
    public interface ICheapPaymentGateway
    {
        public (string, string) ProcessCheapPayment(PaymentDetail paymentDetail);
    }
}
