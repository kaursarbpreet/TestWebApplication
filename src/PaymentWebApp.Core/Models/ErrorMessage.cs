using FluentValidation.Results;

namespace PaymentWebApp.Core.Models
{
    public class ErrorMessage
    {
        public ErrorMessage(ValidationFailure validationFailure)
        {
            this.Property = validationFailure.PropertyName;
            this.Message = validationFailure.ErrorMessage;
        }

        public string Property { get; set; }
        public string Message { get; set; }
    }
}
