using KasirKafe_Kel6.Models;
using System.Collections.Generic;

namespace KasirKafe_Kel6.Services
{
    public class PencetakanService
    {
        public string GenerateNomorResi()
        {
            return Guid.NewGuid().ToString("N").ToUpper().Substring(0, 12);
        }

        public ApiResponse<List<string>> GenerateStruk(Pesanan pesanan)
        {
            if (pesanan == null)
                throw new ArgumentNullException(nameof(pesanan), "Data pesanan tidak boleh kosong saat mencetak struk.");

            string noResi = GenerateNomorResi();
            var lines = new List<string>
            {
                "========== KASIR KAFE KELOMPOK 6 ==========",
                $"NO. RESI    : {noResi}",
                $"TANGGAL     : {DateTime.Now:dd/MM/yyyy HH:mm}",
                "-------------------------------------------",
                $"ITEM        : {pesanan.NamaMenu}",
                $"JUMLAH      : {pesanan.Jumlah}",
                $"TOTAL BAYAR : Rp{pesanan.TotalHarga:N0}",
                $"STATUS      : {pesanan.Status}",
                "===========================================",
                "     Terima Kasih Atas Kunjungan Anda!     "
            };

            return ApiResponse<List<string>>.Ok(lines, "Struk berhasil dibuat.");
        }
    }
}