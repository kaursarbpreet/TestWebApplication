using FluentValidation.TestHelper;
using PaymentWebApp.Core.Models;
using PaymentWebApp.Core.Validations;
using System;
using Xunit;

namespace PaymentWebApp.Core.Tests.Validation
{
    public class PaymentDetailValidatorTests
    {
        private readonly PaymentDetailValidator validator;

        public PaymentDetailValidatorTests()
        {
            this.validator = new PaymentDetailValidator();
        }

        [Fact]
        public void Validator_Returns_Error_For_Required_Fields()
        {
            var paymentDetail = new PaymentDetail();
            this.validator.ShouldHaveValidationErrorFor(p => p.Amount, paymentDetail);
            this.validator.ShouldHaveValidationErrorFor(p => p.CardHolder, paymentDetail)
                                      .WithErrorMessage("Card Holder cannot be empty");
            this.validator.ShouldHaveValidationErrorFor(p => p.CreditCardNumber, paymentDetail)
                                      .WithErrorMessage("Credit Card Number cannot be empty");
            this.validator.ShouldHaveValidationErrorFor(p => p.ExpirationDate, paymentDetail)
                                      .WithErrorMessage("Expiration Date cannot be empty");
        }

        [Theory]
        [InlineData("2025-13-02")]
        [InlineData("55-02-02")]
        [InlineData("20/02/2025")]
        [InlineData("2/2/2025")]
        public void Validator_Does_Not_Accept_InValid_ExpirationDate_Formats(string date)
        {
            var paymentDetail = new PaymentDetail() { ExpirationDate = date };

            this.validator.ShouldHaveValidationErrorFor(p => p.ExpirationDate, paymentDetail)
                                      .WithErrorMessage("Expiration Date is incorrect format, please use YYYY-MM-DD");
        }

        [Fact]
        public void Validator_Accepts_FutureDate()
        {
            var paymentDetail = new PaymentDetail() { ExpirationDate = DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd") };
            this.validator.ShouldNotHaveValidationErrorFor(p => p.ExpirationDate, paymentDetail);
        }

        [Theory]
        [InlineData(1.00, true)]
        [InlineData(0.00, false)]
        [InlineData(-10.00, false)]
        public void Validator_Validates_Amount(decimal amount, bool shouldPass)
        {
            var paymentDetail = new PaymentDetail()
            { Amount = amount };
            if (shouldPass)
                this.validator.ShouldNotHaveValidationErrorFor(p => p.Amount, paymentDetail);
            else
                this.validator.ShouldHaveValidationErrorFor(p => p.Amount, paymentDetail)
                    .WithErrorMessage("Amount must be greater than Zero.");
        }

        [Theory]
        [InlineData("I_More_Than_Three_Char", false)]
        [InlineData("the", true)]
        [InlineData("", false)]
        [InlineData(null, true)]
        public void Validator_Validates_SecurityCode(string securityCode, bool shouldPass)
        {
            var paymentDetail = new PaymentDetail()
            { SecurityCode = securityCode };
            if (shouldPass)
                this.validator.ShouldNotHaveValidationErrorFor(p => p.SecurityCode, paymentDetail);
            else
                this.validator.ShouldHaveValidationErrorFor(p => p.SecurityCode, paymentDetail);
        }

        [Fact]
        public void Validator_Accepts_Valid_Values()
        {
            var paymentDetail = new PaymentDetail()
            {
                CardHolder = "CardHolder",
                CreditCardNumber = "5105 1051 0510 5100",
                Amount = 10.00M,
                ExpirationDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
            };

            this.validator.ShouldNotHaveValidationErrorFor(p => p.Amount, paymentDetail);
            this.validator.ShouldNotHaveValidationErrorFor(p => p.CardHolder, paymentDetail);
            this.validator.ShouldNotHaveValidationErrorFor(p => p.CreditCardNumber, paymentDetail);
            this.validator.ShouldNotHaveValidationErrorFor(p => p.ExpirationDate, paymentDetail);
        }
    }
}
