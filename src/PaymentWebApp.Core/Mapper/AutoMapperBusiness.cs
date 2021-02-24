using AutoMapper;
using PaymentWebApp.Repository.Entities;
using M = PaymentWebApp.Core.Models;

namespace PaymentWebApp.Core.Mapper
{
    public class AutoMapperBusiness : Profile
    {
        public AutoMapperBusiness()
        {
            CreateMap<M.PaymentDetail, PaymentDetail>();
        }
    }
}
