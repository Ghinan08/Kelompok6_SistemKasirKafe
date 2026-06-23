namespace KasirKafe_Kel6.Models
{
    public class KalkulasiHargaRequest
    {
        public int MenuId { get; set; }
        public List<MenuVariasi> Variasi { get; set; } = new();
    }
}