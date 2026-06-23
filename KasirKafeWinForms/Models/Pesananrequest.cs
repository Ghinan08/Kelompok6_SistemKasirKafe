using System.ComponentModel.DataAnnotations;

namespace KasirKafe_Kel6.Models
{
    /// <summary>
    /// Body request untuk POST /api/Pesanan.
    /// Sengaja ditaruh di namespace Models (bukan DTO) di sisi WinForms ini
    /// supaya konsisten dengan model lain di project KasirKafeWinForms
    /// (Menu, ApiResponse, dst semua di Models/).
    /// </summary>
    public class PesananRequest
    {
        [Required]
        public string NamaMenu { get; set; } = string.Empty;

        [Range(1, 1000)]
        public int Jumlah { get; set; }

        [Range(0, 10000000)]
        public decimal TotalHarga { get; set; }
    }
}