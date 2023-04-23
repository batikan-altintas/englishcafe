using System.ComponentModel.DataAnnotations.Schema;

namespace BatikanSon.Models
{
    public class Randevu
    {
        public int RandevuID { get; set; }
        [ForeignKey("Ogretmen")]
        public int? OgretmenID { get; set; }
        [ForeignKey("Ogrenci")]
        public int? OgrenciID { get; set; }
        
        [ForeignKey("RandevuTanimi")]
        public int RTID { get; set; }
        public bool GerceklestiMi { get; set; }


        public RandevuTanimi? RandevuTanimi { get; set; }
        public Uye? Ogretmen { get; set; }
        public Uye? Ogrenci { get; set; }
    }
}
