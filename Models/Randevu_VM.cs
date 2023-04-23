using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BatikanSon.Models
{
    public class Randevu_VM
    {
        public int ID { get; set; }
        public int OgrenciID { get; set; }
        public string OgrenciAdi { get; set; }
        public string OgrenciSoyadi { get; set; }
        [NotMapped]
        [Display(Name = "Öğrenci Ad Soyad")]
        public string OgrenciAdSoyad { get => OgrenciAdi + " " + OgrenciSoyadi; }
        public int? OgretmenID { get; set; }
        public string OgretmenAdi { get; set; }
        public string OgretmenSoyadi { get; set; }
        [NotMapped]
        [Display(Name = "Öğretmen Ad Soyad")]
        public string OgretmenAdSoyad { get => OgretmenAdi + " " + OgretmenSoyadi; }
        [Display(Name = "Dil Adı")]
        public string DilAdi { get; set; }
        [Display(Name = "Mekan Adı")]
        public string MekanAdi { get; set; }
        [Display(Name = "Gün Adı")]
        public string GunAdi { get; set; }
        [Display(Name = "Saat Adı")]
        public string SaatAdi { get; set; }
    }
}
