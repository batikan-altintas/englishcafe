using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatikanSon.Models.Config
{
    public class UyeCFG : IEntityTypeConfiguration<Uye>
    {
        public void Configure(EntityTypeBuilder<Uye> builder)
        {
            builder.Property(x => x.Ad).HasMaxLength(50);
            builder.Property(x => x.Soyad).HasMaxLength(50);
        }
    }
}
