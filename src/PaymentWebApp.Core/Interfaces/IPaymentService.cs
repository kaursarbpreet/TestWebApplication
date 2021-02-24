using PaymentWebApp.Core.Models;
using System.Threading.Tasks;

namespace PaymentWebApp.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<ProcessPaymentResult> ProcessPayment(PaymentDetail paymentDetail);
    }
}
