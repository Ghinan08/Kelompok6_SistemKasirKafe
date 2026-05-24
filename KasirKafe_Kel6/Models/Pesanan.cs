namespace KasirKafe_Kel6.Models
{
    public class Pesanan
    {
        public int Id { get; set; }

        public string NamaMenu { get; set; } = string.Empty;

        public int Jumlah { get; set; }

        public decimal TotalHarga { get; set; }

        public string Status { get; set; } = "Dipesan";
    }
}