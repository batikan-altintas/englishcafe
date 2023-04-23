namespace BatikanSon.Models
{
    public class Mekan
    {
        public int MekanID { get; set; }
        public string MekanAdi { get; set; }


        public ICollection<RandevuTanimi>? RandevuTanimlari { get; set; }
    }
}
