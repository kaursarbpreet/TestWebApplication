using Microsoft.EntityFrameworkCore;
using PaymentWebApp.Repository.Entities;
using PaymentWebApp.Repository.Interfaces;

namespace PaymentWebApp.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        internal PaymentContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(PaymentContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity, bool SaveChanges = true)
        {
            dbSet.Add(entity);
            if (SaveChanges)
                context.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate, bool SaveChanges = true)
        {
            foreach (var entity in context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }

            context.Entry(entityToUpdate).State = EntityState.Modified;
            if (SaveChanges)
                context.SaveChanges();
        }
    }
}
