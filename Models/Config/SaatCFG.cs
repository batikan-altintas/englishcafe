using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatikanSon.Models.Config
{
    public class SaatCFG : IEntityTypeConfiguration<Saat>
    {
        public void Configure(EntityTypeBuilder<Saat> builder)
        {
            builder.HasData(new Saat { SaatID = 1, SaatAdi = "10.00" });
            builder.HasData(new Saat { SaatID = 2, SaatAdi = "11.00" });
            builder.HasData(new Saat { SaatID = 3, SaatAdi = "12.00" });
            builder.HasData(new Saat { SaatID = 4, SaatAdi = "13.00" });
            builder.HasData(new Saat { SaatID = 5, SaatAdi = "14.00" });
            builder.HasData(new Saat { SaatID = 6, SaatAdi = "15.00" });
            builder.HasData(new Saat { SaatID = 7, SaatAdi = "16.00" });
            builder.HasData(new Saat { SaatID = 8, SaatAdi = "17.00" });
            builder.HasData(new Saat { SaatID = 9, SaatAdi = "18.00" });
            builder.HasData(new Saat { SaatID = 10, SaatAdi = "19.00" });
            builder.HasData(new Saat { SaatID = 11, SaatAdi = "20.00" });
            builder.HasData(new Saat { SaatID = 12, SaatAdi = "21.00" });
        }
    }
}
