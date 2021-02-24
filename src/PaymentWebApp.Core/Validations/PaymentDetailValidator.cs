using FluentValidation;
using PaymentWebApp.Core.Models;
using System;

namespace PaymentWebApp.Core.Validations
{
    public class PaymentDetailValidator : AbstractValidator<PaymentDetail>
    {
        public PaymentDetailValidator()
        {
            RuleFor(p => p.CardHolder)
              .Cascade(CascadeMode.Stop)
              .NotEmpty()
              .WithMessage("Card Holder cannot be empty")
              .MaximumLength(100)
              .WithMessage("Card Holder cannot be more than 100 characters");

            RuleFor(p => p.CreditCardNumber)
             .Cascade(CascadeMode.Stop)
             .NotEmpty()
             .WithMessage("Credit Card Number cannot be empty")
             .CreditCard()
             .WithMessage("Credit Card Number is invalid");

            RuleFor(s => s.ExpirationDate)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("Expiration Date cannot be empty")
               .Matches(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))")
               .WithMessage("Expiration Date is incorrect format, please use YYYY-MM-DD")
               .Must(this.ExpirationDateValidate)
               .WithMessage("ExpirationDate can not be in past");

            RuleFor(p => p.SecurityCode)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .When(p => p.SecurityCode != null)
               .WithMessage("Security Code cannot be empty")
               .MaximumLength(3)
               .WithMessage("Security Code cannot be more than 3 characters");

            RuleFor(p => p.Amount)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(00.00M)
                .WithMessage("Amount must be greater than Zero.");
        }

        private bool ExpirationDateValidate(string expirationDate)
        {
            DateTime.TryParse(expirationDate, out var parsed);
            if (parsed >= DateTime.UtcNow.Date)
                return true;
            else
                return false;
        }
    }
}
