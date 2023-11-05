using Ecommerce.Entities;

namespace Ecommerce.DAL.Repositories.AppUser
{
    public interface IAppUserRepository : IRepository<AppUserEntity, long>
    {
        Task<AppUserEntity> GetByEmailAsync(string email, CancellationToken token);

        Task<bool> IsExistsAsync(long appUserId, CancellationToken token);
    }
}