using AutoMapper;
using FluentValidation.Results;
using PaymentWebApp.Core.Interfaces;
using PaymentWebApp.Core.Validations;
using PaymentWebApp.Repository.Entities;
using PaymentWebApp.Repository.Interfaces;
using System.Threading.Tasks;
using M = PaymentWebApp.Core.Models;

namespace PaymentWebApp.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitofWork unitOfWork;
        private readonly ICheapPaymentGateway cheapPaymentGateway;
        private readonly IExpensivePaymentGateway expensivePaymentGateway;
        private readonly IPremiumPaymentService premiumPaymentService;
        private readonly IMapper mapper;

        public PaymentService(IUnitofWork unitOfWork, ICheapPaymentGateway cheapPaymentGateway, IExpensivePaymentGateway expensivePaymentGateway, IPremiumPaymentService premiumPaymentService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.cheapPaymentGateway = cheapPaymentGateway;
            this.expensivePaymentGateway = expensivePaymentGateway;
            this.premiumPaymentService = premiumPaymentService;
            this.mapper = mapper;
        }

        public async Task<M.ProcessPaymentResult> ProcessPayment(M.PaymentDetail paymentDetail)
        {
            var validationResult = await this.Validate(paymentDetail);

            if (!validationResult.IsValid)
            {
                return new M.ProcessPaymentResult(validationResult);
            }
            paymentDetail.PaymentDetailId = this.PaymentDetailInsert(paymentDetail);
            string status;
            string transationId;
            if (paymentDetail.Amount < M.PaymentCapTypes.Cheap)
                (status, transationId) = this.cheapPaymentGateway.ProcessCheapPayment(paymentDetail);
            else if (paymentDetail.Amount > M.PaymentCapTypes.Cheap && paymentDetail.Amount < M.PaymentCapTypes.Expensive)
            {
                (status, transationId) = this.expensivePaymentGateway.ProcessExpensivePayment(paymentDetail);
                if (status == "failed")
                    (status, transationId) = this.cheapPaymentGateway.ProcessCheapPayment(paymentDetail);
            }
            else
                (status, transationId) = this.ProcessPremiumPayment(paymentDetail, 3);

            this.PaymentDetailUpdate(paymentDetail, transationId);
            this.PaymentStateInsert(paymentDetail.PaymentDetailId, status);
            if (status == "failed")
                return new M.ProcessPaymentResult() { IsSuccess = false };
            return new M.ProcessPaymentResult();
        }

        private async Task<ValidationResult> Validate(M.PaymentDetail paymentDetail)
        {
            var validator = new PaymentDetailValidator();
            return await validator.ValidateAsync(paymentDetail);
        }

        private int PaymentDetailInsert(M.PaymentDetail paymentDetail)
        {
            var paymentDetailEntity = new PaymentDetail();
            mapper.Map(paymentDetail, paymentDetailEntity);
            this.unitOfWork.PaymentDetailRepository.Insert(paymentDetailEntity);
            return paymentDetailEntity.PaymentDetailId;
        }

        private (string, string) ProcessPremiumPayment(M.PaymentDetail paymentDetail, int repetition)
        {
            string status;
            string transationId;
            (status, transationId) = this.premiumPaymentService.ProcessPremiumPayment(paymentDetail);
            if (status == "failed" && repetition > 0)
                (status, transationId) = this.ProcessPremiumPayment(paymentDetail, repetition--);
            return (status, transationId);
        }

        private void PaymentDetailUpdate(M.PaymentDetail paymentDetail, string transationId)
        {
            var paymentDetailEntity = new PaymentDetail();
            mapper.Map(paymentDetail, paymentDetailEntity);
            paymentDetailEntity.TransactionId = transationId;
            this.unitOfWork.PaymentDetailRepository.Update(paymentDetailEntity);
        }

        private void PaymentStateInsert(int paymentDetailId, string status)
        {
            var paymentStateEntity = new PaymentState()
            {
                PaymentDetailId = paymentDetailId,
                Status = status
            };
            this.unitOfWork.PaymentStateRepository.Insert(paymentStateEntity);
        }
    }
}
