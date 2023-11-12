using Ecommerce.Common.Extensions;
using Ecommerce.DAL.Data;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.DAL.Repositories.AppUser
{
    [AddService(typeof(IAppUserRepository), ServiceLifetime.Scoped)]
    public class AppUserRepository : BaseRepository<AppUserEntity, long> , IAppUserRepository
    {
        public AppUserRepository(EcommerceDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AppUserEntity> GetByEmailAsync(string email, CancellationToken token)
        {
            return _dbSet
                .FirstOrDefaultAsync(x => x.Email == email && x.DeletedTime == null, token);
        }
        
        public Task<AppUserEntity> GetByUserIdAsync(long userId, CancellationToken token)
        {
            return _dbSet
                .FirstOrDefaultAsync(x => x.Id == userId && x.DeletedTime == null, token);
        }

        public Task<bool> IsExistsAsync(long appUserId, CancellationToken token)
        {
            return _dbSet.AnyAsync(x => x.Id == appUserId, token);
        }
    }
}