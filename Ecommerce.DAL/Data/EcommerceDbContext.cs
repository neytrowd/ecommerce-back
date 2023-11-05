using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ecommerce.DAL.Data
{
    public class EcommerceDbContext: DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(EcommerceDbContext)));
        }
        
        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public void Commit()
        {
            Database.CommitTransaction();
        }

        public void Rollback()
        {
            Database.RollbackTransaction();
        }
    }
}