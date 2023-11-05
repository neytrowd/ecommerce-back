using Ecommerce.Common.Extensions;
using Ecommerce.DAL.Data;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.DAL.Repositories.AppUser
{
    [AddService(typeof(IAppUserRepository), ServiceLifetime.Scoped)]
    public class AppUserEntityRepository : BaseRepository<AppUserEntity, long> , IAppUserRepository
    {
        public AppUserEntityRepository(EcommerceDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AppUserEntity> GetByEmailAsync(string email)
        {
            return _dbSet
                .FirstOrDefaultAsync(x => x.Email == email && x.DeletedTime == null);
        }

        public Task<bool> IsExistsAsync(long appUserId, CancellationToken token)
        {
            return _dbSet.AnyAsync(x => x.Id == appUserId, token);
        }
    }
}