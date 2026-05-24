namespace KasirKafe_Kel6.Models
{
    public class PesananRequest
    {
        public string NamaMenu { get; set; } = string.Empty;

        public int Jumlah { get; set; }

        public decimal TotalHarga { get; set; }
    }
}