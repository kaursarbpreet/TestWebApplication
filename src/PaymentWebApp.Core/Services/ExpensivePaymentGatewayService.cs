using PaymentWebApp.Core.Interfaces;
using PaymentWebApp.Core.Models;

namespace PaymentWebApp.Core.Services
{
    public class ExpensivePaymentGatewayService : IExpensivePaymentGateway
    {
        public (string, string) ProcessExpensivePayment(PaymentDetail paymentDetail)
        {
            //TODO: call third party and return values accroding to third party 's response
            return ("failed", "transactionId");
        }
    }
}
