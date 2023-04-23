using System.ComponentModel.DataAnnotations.Schema;

namespace BatikanSon.Models
{
    public class RandevuTanimi
    {
        public int ID { get; set; }
        [ForeignKey("OgretmenRT")]
        public int? OgretmenID { get; set; }
        
        public int DilID { get; set; }
        public int GunID { get; set; }
        public int SaatID { get; set; }
        public int MekanID { get; set; }
        public bool Onay { get; set; }


        public Dil? Dil { get; set; }
        public Gun? Gun { get; set; }
        public Mekan? Mekan { get; set; }
        public Saat? Saat { get; set; }
        public Randevu? Randevu { get; set; }
        public Uye? OgretmenRT { get; set; }
        
    }
}
