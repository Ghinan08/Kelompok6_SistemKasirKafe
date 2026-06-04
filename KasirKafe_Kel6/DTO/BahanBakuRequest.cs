using System.ComponentModel.DataAnnotations;

namespace KasirKafe_Kel6.DTO
{
    public class BahanBakuRequest
    {
        [Required(ErrorMessage = "Nama bahan tidak boleh kosong.")]
        public string NamaBahan { get; set; } = string.Empty;

        [Range(0, 100000, ErrorMessage = "Stok awal tidak boleh negatif.")]
        public int StokAwal { get; set; }
    }
}