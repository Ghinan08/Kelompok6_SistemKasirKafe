using KasirKafe_Kel6.Models;
using System.Text;
using System.Text.Json;

namespace KasirKafe_Kel6
{
    public partial class FormKasir : Form
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrlKalkulasi = "http://localhost:5011/api/KalkulasiHarga";
        private const string BaseUrlPesanan = "http://localhost:5011/api/Pesanan";
        private const string BaseUrlMenu = "http://localhost:5011/api/Menu";
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        // =====================================================================
        // SECURE CODING: Batas atas jumlah item per variasi DAN jumlah porsi.
        // Ini BUKAN magic number harga (harga tetap murni dari API), melainkan
        // batas KEAMANAN INPUT untuk mencegah:
        //   1. Integer overflow saat backend mengalikan HargaSatuan * Jumlah
        //      (kalau Jumlah dibiarkan sampai int.MaxValue, hasil kali decimal
        //      bisa overflow / menghasilkan angka tidak masuk akal).
        //   2. Manipulasi pesanan yang tidak realistis (siapa yang pesan
        //      99999 Extra Shot dalam satu baris?).
        // Nilai ini didefinisikan SEKALI sebagai konstanta bernama, bukan
        // ditulis berulang sebagai angka mentah di banyak tempat (NumericUpDown
        // di Designer.cs membaca nilai yang sama lewat properti Minimum/Maximum).
        // =====================================================================
        private const int JumlahItemMinimal = 1;
        private const int JumlahItemMaksimal = 99;

        // Cache nama variasi -> harga, hasil dari GET /api/KalkulasiHarga/variasi.
        // INI SATU-SATUNYA SUMBER HARGA VARIASI DI FORM. Tidak ada angka harga
        // lain yang ditulis manual di kode form ini (Clean Code: no magic numbers).
        private Dictionary<string, decimal> _tabelHargaVariasi = new();

        // Menyimpan NumericUpDown yang dibuat dinamis per variasi yang dicentang,
        // supaya bisa dibaca nilainya saat user klik "Hitung Subtotal".
        private readonly Dictionary<string, NumericUpDown> _inputJumlahPerVariasi = new();

        private List<Models.Menu> _daftarMenu = new();
        private KalkulasiResult? _hasilKalkulasiTerakhir;

        public FormKasir()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void FormKasir_Load(object sender, EventArgs e)
        {
            await LoadDaftarMenuAsync();
            await LoadDaftarVariasiAsync();
        }

        /// <summary>
        /// Mengambil data Menu dari modul Orang 1 (endpoint GET /api/Menu) untuk
        /// ditampilkan di halaman Kasir, sesuai pembagian tugas:
        /// "Bertugas mengambil data Menu dari Orang 1 untuk ditampilkan di
        /// halaman Kasir."
        /// </summary>
        private async Task LoadDaftarMenuAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrlMenu);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Gagal memuat daftar menu dari server.",
                        "Error Memuat Menu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<Models.Menu>>>(jsonString, _jsonOptions);

                if (result?.Success == true && result.Data != null)
                {
                    _daftarMenu = result.Data;
                    cmbMenu.DisplayMember = nameof(Models.Menu.Nama);
                    cmbMenu.ValueMember = nameof(Models.Menu.Id);
                    cmbMenu.DataSource = _daftarMenu;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Gagal memuat data menu: {ex.Message}",
                    "Error Jaringan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// CLO2 - Integrasi API: GET /api/KalkulasiHarga/variasi
        /// Mengambil daftar variasi & harganya MURNI dari backend.
        /// Tidak ada satu pun nilai harga variasi yang di-hardcode di sini.
        /// </summary>
        private async Task LoadDaftarVariasiAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrlKalkulasi}/variasi");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Gagal memuat daftar variasi dari server.",
                        "Error Memuat Variasi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<Dictionary<string, decimal>>>(jsonString, _jsonOptions);

                if (result?.Success == true && result.Data != null)
                {
                    _tabelHargaVariasi = result.Data;
                    BuildVariasiChecklist();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Gagal memuat data variasi: {ex.Message}",
                    "Error Jaringan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Membangun isi CheckedListBox dari _tabelHargaVariasi (hasil API),
        /// urut alfabet agar mudah dicari kasir saat sibuk. Harga yang
        /// ditampilkan di label diambil langsung dari _tabelHargaVariasi,
        /// bukan ditulis manual.
        /// </summary>
        private void BuildVariasiChecklist()
        {
            clbVariasi.Items.Clear();
            pnlVariasiJumlah.Controls.Clear();
            _inputJumlahPerVariasi.Clear();

            foreach (var namaVariasi in _tabelHargaVariasi.Keys.OrderBy(k => k))
            {
                decimal harga = _tabelHargaVariasi[namaVariasi];
                clbVariasi.Items.Add($"{namaVariasi} (+Rp {harga:N0})");
            }
        }

        /// <summary>
        /// Saat user mencentang/membuka centang sebuah variasi, tambahkan/hapus
        /// NumericUpDown jumlah untuk variasi tersebut.
        /// SECURE CODING: NumericUpDown dibatasi Minimum/Maximum di sini,
        /// sehingga secara teknis TIDAK MUNGKIN user mengetik nilai di luar
        /// rentang yang valid -- ini mencegah jumlah negatif atau jumlah
        /// raksasa yang bisa merusak kalkulasi/menyebabkan overflow di backend.
        /// </summary>
        private void clbVariasi_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string namaVariasi = _tabelHargaVariasi.Keys.OrderBy(k => k).ElementAt(e.Index);

            // BeginInvoke supaya state CheckedListBox sudah ter-update dulu
            // sebelum kita membaca ulang status centang (hindari race condition UI).
            BeginInvoke(new Action(() =>
            {
                if (e.NewValue == CheckState.Checked)
                    TambahInputJumlahVariasi(namaVariasi);
                else
                    HapusInputJumlahVariasi(namaVariasi);
            }));
        }

        private void TambahInputJumlahVariasi(string namaVariasi)
        {
            if (_inputJumlahPerVariasi.ContainsKey(namaVariasi)) return;

            int posisi = _inputJumlahPerVariasi.Count;

            var lbl = new Label
            {
                Text = namaVariasi,
                AutoSize = true,
                ForeColor = Color.White,
                Location = new Point(8, 10 + posisi * 32)
            };

            var nud = new NumericUpDown
            {
                // ===== SECURE CODING: batasan anti integer overflow =====
                Minimum = JumlahItemMinimal,
                Maximum = JumlahItemMaksimal,
                Value = JumlahItemMinimal,
                Location = new Point(8, 10 + posisi * 32 + 16),
                Width = 80,
                Name = namaVariasi
            };

            pnlVariasiJumlah.Controls.Add(lbl);
            pnlVariasiJumlah.Controls.Add(nud);
            _inputJumlahPerVariasi[namaVariasi] = nud;
        }

        private void HapusInputJumlahVariasi(string namaVariasi)
        {
            if (!_inputJumlahPerVariasi.TryGetValue(namaVariasi, out var nud)) return;

            pnlVariasiJumlah.Controls.Remove(nud);

            var label = pnlVariasiJumlah.Controls
                .OfType<Label>()
                .FirstOrDefault(l => l.Text == namaVariasi);
            if (label != null) pnlVariasiJumlah.Controls.Remove(label);

            _inputJumlahPerVariasi.Remove(namaVariasi);
            SusunUlangPanelJumlahVariasi();
        }

        private void SusunUlangPanelJumlahVariasi()
        {
            int i = 0;
            foreach (var kv in _inputJumlahPerVariasi)
            {
                kv.Value.Location = new Point(8, 10 + i * 32 + 16);
                var label = pnlVariasiJumlah.Controls
                    .OfType<Label>()
                    .FirstOrDefault(l => l.Text == kv.Key);
                if (label != null) label.Location = new Point(8, 10 + i * 32);
                i++;
            }
        }

        private void cmbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset hasil kalkulasi lama saat menu berganti, agar kasir tidak
            // salah kirim pesanan dengan subtotal menu sebelumnya.
            ResetHasilKalkulasi();
        }

        private void ResetHasilKalkulasi()
        {
            _hasilKalkulasiTerakhir = null;
            btnKirimPesanan.Enabled = false;
            lvRincian.Items.Clear();
            lblHargaDasarValue.Text = "Rp 0";
            lblTotalTambahanValue.Text = "Rp 0";
            lblTotalAkhirValue.Text = "Rp 0";
        }

        /// <summary>
        /// CLO2 - Integrasi API: POST /api/KalkulasiHarga/hitung
        /// Mengumpulkan menuId + variasi terpilih, kirim ke backend, lalu
        /// tampilkan hasilnya APA ADANYA (tidak ada perhitungan ulang di FE).
        /// </summary>
        private async void btnHitungSubtotal_Click(object sender, EventArgs e)
        {
            if (cmbMenu.SelectedItem is not Models.Menu menuTerpilih)
            {
                MessageBox.Show(
                    "Silakan pilih menu terlebih dahulu.",
                    "Validasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var request = new KalkulasiHargaRequest
            {
                MenuId = menuTerpilih.Id,
                Variasi = KumpulkanVariasiTerpilih()
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            btnHitungSubtotal.Enabled = false;
            try
            {
                var response = await _httpClient.PostAsync($"{BaseUrlKalkulasi}/hitung", jsonContent);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<ApiResponse<KalkulasiResult>>(jsonString, _jsonOptions);

                    if (result?.Success == true && result.Data != null)
                    {
                        _hasilKalkulasiTerakhir = result.Data;
                        TampilkanHasilKalkulasi(result.Data);
                        btnKirimPesanan.Enabled = true;
                    }
                }
                else
                {
                    // Pesan error di sini berasal dari backend (mis. "Variasi 'X'
                    // tidak dikenal", "Menu tidak ditemukan") -- ditampilkan
                    // langsung ke kasir tanpa form menebak-nebak alasannya sendiri.
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<string>>(jsonString, _jsonOptions);
                    MessageBox.Show(
                        errorResult?.Message ?? "Gagal menghitung harga.",
                        "Gagal Menghitung Harga",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    ResetHasilKalkulasi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Terjadi kesalahan koneksi: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnHitungSubtotal.Enabled = true;
            }
        }

        /// <summary>
        /// Membaca NumericUpDown yang sedang aktif (variasi yang dicentang) dan
        /// mengubahnya jadi List&lt;MenuVariasi&gt; sesuai kontrak request backend.
        /// Karena NumericUpDown sudah dibatasi Minimum/Maximum saat dibuat,
        /// nilai yang terbaca di sini sudah pasti dalam rentang aman, tapi tetap
        /// di-clamp ulang sebagai lapis pertahanan kedua sebelum dikirim.
        /// </summary>
        private List<MenuVariasi> KumpulkanVariasiTerpilih()
        {
            var hasil = new List<MenuVariasi>();

            foreach (var kv in _inputJumlahPerVariasi)
            {
                int jumlahAman = Math.Clamp((int)kv.Value.Value, JumlahItemMinimal, JumlahItemMaksimal);

                hasil.Add(new MenuVariasi
                {
                    NamaVariasi = kv.Key,
                    Jumlah = jumlahAman
                });
            }

            return hasil;
        }

        /// <summary>
        /// Render hasil dari backend ke UI. Setiap nilai (HargaDasar, HargaSatuan,
        /// Subtotal, TotalTambahan, TotalAkhir) diambil langsung dari KalkulasiResult,
        /// TIDAK dihitung ulang di sisi Form -- memastikan satu sumber kebenaran
        /// harga ada di backend, sesuai requirement "tidak boleh hardcoded".
        /// </summary>
        private void TampilkanHasilKalkulasi(KalkulasiResult hasil)
        {
            lvRincian.Items.Clear();

            foreach (var rincian in hasil.RincianVariasi)
            {
                var item = new ListViewItem(rincian.NamaVariasi);
                item.SubItems.Add(rincian.Jumlah.ToString());
                item.SubItems.Add($"Rp {rincian.HargaSatuan:N0}");
                item.SubItems.Add($"Rp {rincian.Subtotal:N0}");
                lvRincian.Items.Add(item);
            }

            lblHargaDasarValue.Text = $"Rp {hasil.HargaDasar:N0}";
            lblTotalTambahanValue.Text = $"Rp {hasil.TotalTambahan:N0}";
            lblTotalAkhirValue.Text = $"Rp {hasil.TotalAkhir:N0}";
        }

        /// <summary>
        /// CLO2 - Integrasi API: POST /api/Pesanan
        /// Mengirim pesanan final ke backend setelah kasir mengonfirmasi
        /// subtotal dari POST /api/KalkulasiHarga/hitung.
        ///
        /// CATATAN DESAIN: endpoint Pesanan hanya menyimpan NamaMenu, Jumlah
        /// (porsi), dan TotalHarga -- TIDAK menyimpan rincian variasi sebagai
        /// item terpisah (sudah dikonfirmasi final oleh pemilik modul Pesanan).
        /// Rincian variasi yang tampil di lvRincian hanya untuk transparansi
        /// harga ke kasir, tidak ikut tersimpan ke database Pesanan.
        ///
        /// TotalHarga yang dikirim = TotalAkhir per satu porsi (dari hasil
        /// kalkulasi backend) dikali Jumlah porsi -- bukan dihitung manual
        /// dengan angka baru, supaya tidak ada magic number perhitungan baru
        /// muncul di Form.
        /// </summary>
        private async void btnKirimPesanan_Click(object sender, EventArgs e)
        {
            if (_hasilKalkulasiTerakhir == null || cmbMenu.SelectedItem is not Models.Menu menuTerpilih)
            {
                MessageBox.Show(
                    "Hitung subtotal terlebih dahulu sebelum mengirim pesanan.",
                    "Validasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // SECURE CODING: jumlah porsi dibaca dari NumericUpDown yang sudah
            // dibatasi Minimum/Maximum di Designer, lalu di-clamp ulang sebagai
            // lapis pertahanan kedua sebelum dikirim ke backend.
            int jumlahPorsi = Math.Clamp((int)nudJumlahPorsi.Value, JumlahItemMinimal, JumlahItemMaksimal);
            decimal totalHargaSemuaPorsi = _hasilKalkulasiTerakhir.TotalAkhir * jumlahPorsi;

            var requestBody = new PesananRequest
            {
                NamaMenu = menuTerpilih.Nama,
                Jumlah = jumlahPorsi,
                TotalHarga = totalHargaSemuaPorsi
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            btnKirimPesanan.Enabled = false;
            try
            {
                var response = await _httpClient.PostAsync(BaseUrlPesanan, jsonContent);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<ApiResponse<Pesanan>>(jsonString, _jsonOptions);

                    MessageBox.Show(
                        $"Pesanan berhasil dikirim ke dapur!\n\n" +
                        $"Menu: {result?.Data?.NamaMenu}\n" +
                        $"Jumlah: {result?.Data?.Jumlah} porsi\n" +
                        $"Total: Rp {result?.Data?.TotalHarga:N0}\n" +
                        $"Status: {result?.Data?.Status}",
                        "Pesanan Terkirim",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ResetFormSetelahPesananTerkirim();
                }
                else
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<string>>(jsonString, _jsonOptions);
                    MessageBox.Show(
                        errorResult?.Message ?? "Gagal mengirim pesanan.",
                        "Gagal Mengirim Pesanan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    btnKirimPesanan.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Terjadi kesalahan koneksi: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                btnKirimPesanan.Enabled = true;
            }
        }

        /// <summary>
        /// Reset tampilan ke kondisi awal setelah pesanan berhasil dikirim,
        /// supaya kasir bisa langsung melayani pesanan berikutnya tanpa risiko
        /// tidak sengaja kirim ulang pesanan yang sama.
        /// </summary>
        private void ResetFormSetelahPesananTerkirim()
        {
            ResetHasilKalkulasi();
            nudJumlahPorsi.Value = JumlahItemMinimal;

            for (int i = 0; i < clbVariasi.Items.Count; i++)
            {
                clbVariasi.SetItemChecked(i, false);
            }
        }
    }
}