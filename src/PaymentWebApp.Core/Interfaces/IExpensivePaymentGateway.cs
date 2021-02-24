using PaymentWebApp.Core.Models;

namespace PaymentWebApp.Core.Interfaces
{
    public interface IExpensivePaymentGateway
    {
        public (string, string) ProcessExpensivePayment(PaymentDetail paymentDetail);
    }
}
