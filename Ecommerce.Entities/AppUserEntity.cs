using Ecommerce.Entities.Abstractions;

namespace Ecommerce.Entities
{
    public class AppUserEntity : IEntityBase<long>
    {
        public long Id { get; set; }
        
        public string Email { get; set; }
        
        public string HashPassword { get; set; }
    }
}