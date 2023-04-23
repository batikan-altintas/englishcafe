using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatikanSon.Models
{
    public class Uye : IdentityUser<int>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }


        [InverseProperty("Ogretmen")]
        public ICollection<Randevu>? Ogretmen { get; set; }
        [InverseProperty("Ogrenci")]
        public ICollection<Randevu>? Ogrenci { get; set; }
        [InverseProperty("OgretmenRT")]
        public ICollection<RandevuTanimi>? OgretmenRT { get; set; }
        
    }
}
