namespace PaymentWebApp.Repository.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity, bool SaveChanges = true);
        void Update(TEntity entityToUpdate, bool SaveChanges = true);
    }
}
