using PaymentWebApp.Repository.Entities;
using System;

namespace PaymentWebApp.Repository.Interfaces
{
    public interface IUnitofWork : IDisposable
    {
        PaymentContext GetContext();

        void Save();

        IBaseRepository<PaymentDetail> PaymentDetailRepository
        {
            get;
        }

        IBaseRepository<PaymentState> PaymentStateRepository
        {
            get;
        }
    }
}
