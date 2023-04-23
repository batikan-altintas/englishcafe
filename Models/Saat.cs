namespace BatikanSon.Models
{
    public class Saat
    {
        public int SaatID { get; set; }
        public string SaatAdi { get; set; }


        public ICollection<RandevuTanimi>? RandevuTanimlari { get; set; }
    }
}
