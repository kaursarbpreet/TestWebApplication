using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentWebApp.Core.Interfaces;
using PaymentWebApp.Core.Models;
using System.Threading.Tasks;

namespace PaymentWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
       
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDetail paymentDetail)
        {
            var processPaymentResult = await this.paymentService.ProcessPayment(paymentDetail);
            if (processPaymentResult.IsSuccess)
                return this.OkMessage("Payment processed successfully!");
            else if (processPaymentResult.Errors != null)
                return new BadRequestObjectResult(processPaymentResult);
            else
                return this.BadRequestMessage("Payment Failed!");
        }
    }
}
