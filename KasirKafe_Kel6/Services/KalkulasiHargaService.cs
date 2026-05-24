using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;

namespace KasirKafe_Kel6.Services
{
    public class KalkulasiHargaService
    {
        private readonly IRepository<Menu> _menuRepo;

        private static readonly Dictionary<string, decimal> _tabelHargaVariasi
            = new(StringComparer.OrdinalIgnoreCase)
            {
                {"Extra Shot",5_000},
                {"Oat Milk",8_000},
                {"Susu Full Cream",4_000},
                {"Whipped Cream",6_000},
                {"Caramel Drizzle",4_000},
                {"Coklat Bubuk",3_000},
                {"Boba",5_000},
                {"Jelly",4_000},
                {"Small",0},
                {"Medium",3_000},
                {"Large",6_000},
                {"Extra Large",9_000},
            };

        public KalkulasiHargaService(IRepository<Menu> menuRepo)
        {
            _menuRepo = menuRepo ?? throw new ArgumentNullException(nameof(menuRepo),
                "Repository menu tidak boleh null.");
        }

        public Dictionary<string, decimal> GetTabelVariasi()
        {
            return new Dictionary<string, decimal>(_tabelHargaVariasi, StringComparer.OrdinalIgnoreCase);
        }

        public KalkulasiResult HitungTotal(int menuId, List<MenuVariasi> variasi)
        {
            if (menuId <= 0)
                throw new ArgumentException(
                    $"menuId harus lebih besar dari 0. Nilai diterima: {menuId}", nameof(menuId));

            if (variasi == null)
                throw new ArgumentNullException(nameof(variasi),
                    "Daftar variasi tidak boleh null. Kirim list kosong jika tidak ada variasi.");

            var menu = _menuRepo.GetById(menuId)
                ?? throw new KeyNotFoundException($"Menu dengan ID {menuId} tidak ditemukan.");

            if (menu.Harga < 0)
                throw new InvalidOperationException(
                    $"Data tidak valid: harga menu '{menu.Nama}' bernilai negatif ({menu.Harga}).");

            var rincian = new List<RincianVariasi>();
            decimal totalTambahan = 0;

            foreach (var v in variasi)
            {
                if (string.IsNullOrWhiteSpace(v.NamaVariasi))
                    throw new ArgumentException("Nama variasi tidak boleh kosong atau hanya spasi.");

                if (v.Jumlah <= 0)
                    throw new ArgumentException(
                        $"Jumlah variasi '{v.NamaVariasi}' harus lebih dari 0. Nilai diterima: {v.Jumlah}");

                if (!_tabelHargaVariasi.TryGetValue(v.NamaVariasi, out decimal hargaSatuan))
                    throw new KeyNotFoundException(
                        $"Variasi '{v.NamaVariasi}' tidak dikenal. " +
                        $"Gunakan GET /api/KalkulasiHarga/variasi untuk melihat daftar variasi.");

                decimal subtotal = hargaSatuan * v.Jumlah;
                totalTambahan += subtotal;

                rincian.Add(new RincianVariasi
                {
                    NamaVariasi = v.NamaVariasi,
                    Jumlah = v.Jumlah,
                    HargaSatuan = hargaSatuan,
                    Subtotal = subtotal
                });
            }

            decimal totalAkhir = menu.Harga + totalTambahan;

            if (totalAkhir < 0)
                throw new InvalidOperationException(
                    "Kalkulasi menghasilkan total negatif — ada kesalahan data variasi.");

            return new KalkulasiResult
            {
                NamaMenu = menu.Nama,
                HargaDasar = menu.Harga,
                RincianVariasi = rincian,
                TotalTambahan = totalTambahan,
                TotalAkhir = totalAkhir
            };
        }
    }
}