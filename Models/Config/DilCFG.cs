using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatikanSon.Models.Config
{
    public class DilCFG : IEntityTypeConfiguration<Dil>
    {
        public void Configure(EntityTypeBuilder<Dil> builder)
        {
            builder.Property(x => x.DilAdi).HasMaxLength(50).HasColumnType("varchar");
            builder.HasData(new Dil { DilID = 1, DilAdi = "İngilizce" });
            builder.HasData(new Dil { DilID = 2, DilAdi = "Almanca" });
        }
    }
}
