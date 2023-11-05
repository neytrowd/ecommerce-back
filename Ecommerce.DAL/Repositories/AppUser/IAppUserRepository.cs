using Ecommerce.Entities;

namespace Ecommerce.DAL.Repositories.AppUser
{
    public interface IAppUserRepository : IRepository<AppUserEntity, long>
    {
        Task<AppUserEntity> GetByEmailAsync(string email);

        Task<bool> IsExistsAsync(long appUserId, CancellationToken token);
    }
}