using Microsoft.Extensions.DependencyInjection;
using PaymentWebApp.Core.Interfaces;
using PaymentWebApp.Core.Services;
using PaymentWebApp.Repository.Interfaces;
using PaymentWebApp.Repository.Repositories;

namespace PaymentWebApp.Core.Mapper
{
    public static class DependencyMapper
    {
        public static void Map(IServiceCollection services)
        {
            services.AddScoped<IUnitofWork, UnitOfWork>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICheapPaymentGateway, CheapPaymentGatewayService>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGatewayService>();
            services.AddScoped<IPremiumPaymentService, PremiumPaymentService>();
        }
    }
}
