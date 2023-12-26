using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models.ItemRelatedModels;
using WebApplication1.Enums.ItemEnums;

namespace WebApplication1.Data
{
    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.GUID); // Assuming GUID is your primary key

            builder.Property(i => i.ItemType).IsRequired();

            builder.HasDiscriminator(i => i.ItemType)
                   .HasValue<Film>(ItemType.Film)
                   .HasValue<Camera>(ItemType.Camera);
        }
    }
}
