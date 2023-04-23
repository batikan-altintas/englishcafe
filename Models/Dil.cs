namespace BatikanSon.Models
{
    public class Dil
    {
        public int DilID { get; set; }
        public string DilAdi { get; set; }


        public ICollection<RandevuTanimi>? RandevuTanimlari { get; set; }
    }
}
