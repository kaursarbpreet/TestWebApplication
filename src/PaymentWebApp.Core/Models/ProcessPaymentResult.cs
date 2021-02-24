using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace PaymentWebApp.Core.Models
{
    public class ProcessPaymentResult
    {
        public ProcessPaymentResult()
        {
            this.IsSuccess = true;
        }

        public ProcessPaymentResult(ValidationResult result)
        {
            this.Errors = result.Errors.Select(s => new ErrorMessage(s));
            this.IsSuccess = false;
            this.ValidationResult = result;
        }

        [JsonIgnore]
        public bool IsSuccess { get; set; }
        [JsonIgnore]
        public ValidationResult ValidationResult { get; }
        public IEnumerable<ErrorMessage> Errors { get; set; }
    }
}
