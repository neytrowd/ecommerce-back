using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.DAL.Data.Configuration
{
    public class EmailTokenEntityConfiguration : IEntityTypeConfiguration<EmailTokenEntity>
    {
        public void Configure(EntityTypeBuilder<EmailTokenEntity> builder)
        {
            builder.ToTable("EmailTokens");
        }
    }
}