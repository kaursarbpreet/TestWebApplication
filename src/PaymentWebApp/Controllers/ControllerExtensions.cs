using Microsoft.AspNetCore.Mvc;

namespace PaymentWebApp.Controllers
{
    public static class ControllerExtensions
    {
        public static IActionResult OkMessage(this ControllerBase controller, string message = "OK")
        {
            return controller.Ok(new { message });
        }

        public static IActionResult BadRequestMessage(this ControllerBase controller, string message = "Bad Request")
        {
            return controller.BadRequest(new { message });
        }
    }
}
