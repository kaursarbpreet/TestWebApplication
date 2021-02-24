using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PaymentWebApp.Repository.Entities
{
    public partial class PaymentContext : DbContext
    {
        public readonly IConfiguration Configuration;
        public PaymentContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual DbSet<PaymentDetail> PaymentDetail { get; set; }
        public virtual DbSet<PaymentState> PaymentState { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}

