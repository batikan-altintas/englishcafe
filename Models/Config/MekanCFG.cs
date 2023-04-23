using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatikanSon.Models.Config
{
    public class MekanCFG : IEntityTypeConfiguration<Mekan>
    {
        public void Configure(EntityTypeBuilder<Mekan> builder)
        {
            builder.HasData(new Mekan { MekanID = 1, MekanAdi = "Kadıköy Cafe Nero" });
        }
    }
}
