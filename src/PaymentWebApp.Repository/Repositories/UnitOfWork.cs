using PaymentWebApp.Repository.Entities;
using PaymentWebApp.Repository.Interfaces;

namespace PaymentWebApp.Repository.Repositories
{
    public class UnitOfWork : IUnitofWork
    {
        private readonly PaymentContext _context;
        private bool _isDisposed;

        public UnitOfWork(PaymentContext context)
        {
            this._context = context;
        }

        public PaymentContext GetContext()
        {
            return this._context;
        }

        public void Dispose()
        {
            if (_context != null && !_isDisposed)
            {
                _context.Dispose();
                _isDisposed = true;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public IBaseRepository<PaymentDetail> PaymentDetailRepository
        {
            get { return new BaseRepository<PaymentDetail>(GetContext()); }
        }

        public IBaseRepository<PaymentState> PaymentStateRepository
        {
            get { return new BaseRepository<PaymentState>(GetContext()); }
        }
    }
}
