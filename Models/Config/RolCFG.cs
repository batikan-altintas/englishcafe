using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatikanSon.Models.Config
{
    public class RolCFG : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasData(new Rol { Id = 1, Name = "Ogretmen" , NormalizedName="OGRETMEN"});
            builder.HasData(new Rol { Id = 2, Name = "Ogrenci", NormalizedName="OGRENCI" });
            builder.HasData(new Rol { Id = 3, Name = "Admin", NormalizedName="ADMIN" });
        }
    }
}
