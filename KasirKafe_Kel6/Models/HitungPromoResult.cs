namespace KasirKafe_Kel6.Models
{
    public class HitungPromoResult
    {
        public decimal HargaAwal { get; set; }
        public string NamaHari { get; set; } = string.Empty;
        public double PersenDiskon { get; set; }
        public decimal NominalDiskon { get; set; }
        public decimal Pajak { get; set; }
        public decimal TotalAkhir { get; set; }
        public string NamaKasir { get; set; } = string.Empty;
        public string UcapanStruk { get; set; } = string.Empty;
    }
}