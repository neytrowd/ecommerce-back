using Ecommerce.Entities.Abstractions;
using Ecommerce.Enums;

namespace Ecommerce.Entities
{
    public class EmailTokenEntity : IEntityBase<long>
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public EmailType EmailType { get; set; }

        public DateTime CreatedTime { get; set; }

        public long AppUserId { get; set; }

        public AppUserEntity AppUser { get; set; }
    }
}