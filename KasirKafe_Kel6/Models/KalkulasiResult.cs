namespace KasirKafe_Kel6.Models
{
    public class KalkulasiResult
    {
        public string NamaMenu { get; set; } = string.Empty;
        public decimal HargaDasar { get; set; }
        public List<RincianVariasi> RincianVariasi { get; set; } = new();
        public decimal TotalTambahan { get; set; }
        public decimal TotalAkhir { get; set; }
    }
    public class RincianVariasi
    {
        public string NamaVariasi { get; set; } = string.Empty;
        public int Jumlah { get; set; }
        public decimal HargaSatuan { get; set; }
        public decimal Subtotal { get; set; }
    }
}