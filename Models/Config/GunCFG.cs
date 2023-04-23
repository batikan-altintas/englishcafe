using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatikanSon.Models.Config
{
    public class GunCFG : IEntityTypeConfiguration<Gun>
    {
        public void Configure(EntityTypeBuilder<Gun> builder)
        {
            builder.Property(x => x.GunAdi).HasMaxLength(50).HasColumnType("varchar");
            builder.HasData(new Gun { GunID = 1, GunAdi = "Pazartesi" });
            builder.HasData(new Gun { GunID = 2, GunAdi = "Salı" });
            builder.HasData(new Gun { GunID = 3, GunAdi = "Çarşamba" });
            builder.HasData(new Gun { GunID = 4, GunAdi = "Perşembe" });
            builder.HasData(new Gun { GunID = 5, GunAdi = "Cuma" });
            builder.HasData(new Gun { GunID = 6, GunAdi = "Cumartesi" });
            builder.HasData(new Gun { GunID = 7, GunAdi = "Pazar" });
        }
    }
}
