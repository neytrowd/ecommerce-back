using Ecommerce.Entities.Abstractions;

namespace Ecommerce.Entities
{
    public class AppUserEntity : IEntityBase<long>
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }
        
        public bool IsEmailConfirmed { get; set; }

        public string HashedPassword { get; set; }

        public DateTime CreatedTime { get; set; }
        
        public DateTime? DeletedTime { get; set; }
    }
}