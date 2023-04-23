using BatikanSon.Models;
using BatikanSon.Models.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BatikanSon.DAL
{
    public class LangDB : IdentityDbContext<Uye, Rol, int>
    {
        public LangDB(DbContextOptions<LangDB> options) : base(options) 
        {

        }
        public DbSet<Dil> Diller { get; set; }
        public DbSet<Gun> Gunler { get; set; }
        public DbSet<Mekan> Mekanlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<RandevuTanimi> RandevuTanimlari { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Saat> Saatler { get; set; }
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Randevu_VM> Randevu_VMs { get; set; }
        public DbSet<Randevu_Detay> Randevu_Detays { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<Dil>(new DilCFG());
            builder.ApplyConfiguration<Gun>(new GunCFG());
            builder.ApplyConfiguration<Mekan>(new MekanCFG());
            builder.ApplyConfiguration<Rol>(new RolCFG());
            builder.ApplyConfiguration<Saat>(new SaatCFG());
            builder.ApplyConfiguration<Uye>(new UyeCFG());

            base.OnModelCreating(builder);
        }

    }
}
