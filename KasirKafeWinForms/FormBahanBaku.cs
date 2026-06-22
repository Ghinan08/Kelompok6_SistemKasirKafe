using KasirKafe_Kel6.Models;
using System.Text;
using System.Text.Json;

namespace KasirKafe_Kel6
{
    public partial class FormBahanBaku : Form
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5011/api/BahanBaku";
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public FormBahanBaku()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void FormBahanBaku_Load(object sender, EventArgs e)
        {
            await RefreshGridBahanBaku();
        }

        private async Task RefreshGridBahanBaku()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                if (!response.IsSuccessStatusCode) return;

                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<BahanBaku>>>(jsonString, _jsonOptions);

                if (result?.Success == true && result.Data != null)
                {
                    dgvBahanBaku.DataSource = null;
                    dgvBahanBaku.DataSource = result.Data;
                    FormatKolomGrid();
                    WarnaiStatusGrid();
                    lblJumlahData.Text = $"Total: {result.Data.Count} bahan baku";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Gagal memuat data bahan baku: {ex.Message}",
                    "Error Jaringan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatKolomGrid()
        {
            if (dgvBahanBaku.Columns.Count == 0) return;

            if (dgvBahanBaku.Columns.Contains("Id"))
            {
                dgvBahanBaku.Columns["Id"].ReadOnly = true;
                dgvBahanBaku.Columns["Id"].Width = 45;
                dgvBahanBaku.Columns["Id"].HeaderText = "ID";
            }
            if (dgvBahanBaku.Columns.Contains("NamaBahan"))
            {
                dgvBahanBaku.Columns["NamaBahan"].HeaderText = "Nama Bahan";
                dgvBahanBaku.Columns["NamaBahan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvBahanBaku.Columns.Contains("Stok"))
            {
                dgvBahanBaku.Columns["Stok"].HeaderText = "Stok";
                dgvBahanBaku.Columns["Stok"].Width = 80;
            }
            if (dgvBahanBaku.Columns.Contains("Status"))
            {
                dgvBahanBaku.Columns["Status"].HeaderText = "Status Automata";
                dgvBahanBaku.Columns["Status"].Width = 150;
            }

            dgvBahanBaku.ReadOnly = true;
        }

        private void WarnaiStatusGrid()
        {
            foreach (DataGridViewRow row in dgvBahanBaku.Rows)
            {
                if (row.Cells["Status"].Value == null) continue;
                string status = row.Cells["Status"].Value.ToString() ?? string.Empty;

                row.DefaultCellStyle.BackColor = status switch
                {
                    "Habis"          => Color.FromArgb(139, 0, 0),
                    "Menipis"        => Color.FromArgb(160, 90, 0),
                    "DiRestock"      => Color.FromArgb(0, 80, 130),
                    "TersediaBanyak" => Color.FromArgb(0, 90, 45),
                    _                => Color.FromArgb(50, 50, 50)
                };
                row.DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private async void btnTambahBahan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaBahan.Text))
            {
                MessageBox.Show(
                    "Nama bahan tidak boleh kosong atau hanya berisi spasi!",
                    "Validasi Gagal",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNamaBahan.Focus();
                return;
            }

            var requestBody = new
            {
                namaBahan = txtNamaBahan.Text.Trim(),
                stokAwal  = (int)nudStokAwal.Value
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            try
            {
                var response = await _httpClient.PostAsync(BaseUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        $"Bahan baku '{txtNamaBahan.Text.Trim()}' berhasil ditambahkan!",
                        "Sukses",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtNamaBahan.Clear();
                    nudStokAwal.Value = 0;
                    await RefreshGridBahanBaku();
                }
                else
                {
                    var errorJson = await response.Content.ReadAsStringAsync();
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<string>>(errorJson, _jsonOptions);
                    MessageBox.Show(
                        errorResult?.Message ?? "Gagal menambahkan bahan baku.",
                        "Gagal",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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
        }

        private async void btnUpdateStok_Click(object sender, EventArgs e)
        {
            if (dgvBahanBaku.CurrentRow == null)
            {
                MessageBox.Show(
                    "Silakan pilih bahan baku dari tabel terlebih dahulu.",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int idBahan       = Convert.ToInt32(dgvBahanBaku.CurrentRow.Cells["Id"].Value);
            string namaBahan  = dgvBahanBaku.CurrentRow.Cells["NamaBahan"].Value?.ToString() ?? "-";
            int perubahanStok = (int)nudJumlahPerubahan.Value;

            if (perubahanStok == 0)
            {
                MessageBox.Show(
                    "Jumlah perubahan stok tidak boleh 0!",
                    "Validasi Gagal",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string keteranganAksi = perubahanStok > 0
                ? $"MENAMBAH {perubahanStok} stok"
                : $"MENGURANGI {Math.Abs(perubahanStok)} stok";

            var konfirmasi = MessageBox.Show(
                $"Anda akan {keteranganAksi} untuk bahan baku '{namaBahan}'.\nLanjutkan?",
                "Konfirmasi Update Stok",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                string url    = $"{BaseUrl}/{idBahan}/update-stok?perubahanStok={perubahanStok}";
                var response  = await _httpClient.PutAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result     = JsonSerializer.Deserialize<ApiResponse<BahanBaku>>(jsonString, _jsonOptions);
                    string statusBaru = result?.Data?.Status ?? "-";

                    MessageBox.Show(
                        $"Stok '{namaBahan}' berhasil diperbarui!\nStatus Automata sekarang: {statusBaru}",
                        "Sukses",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    nudJumlahPerubahan.Value = 0;
                    await RefreshGridBahanBaku();
                }
                else
                {
                    var errorJson  = await response.Content.ReadAsStringAsync();
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<string>>(errorJson, _jsonOptions);
                    MessageBox.Show(
                        errorResult?.Message ?? "Gagal mengupdate stok bahan baku.",
                        "Gagal",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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
        }

        private async void btnHapusBahan_Click(object sender, EventArgs e)
        {
            if (dgvBahanBaku.CurrentRow == null)
            {
                MessageBox.Show(
                    "Silakan pilih bahan baku dari tabel terlebih dahulu.",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int idBahan      = Convert.ToInt32(dgvBahanBaku.CurrentRow.Cells["Id"].Value);
            string namaBahan = dgvBahanBaku.CurrentRow.Cells["NamaBahan"].Value?.ToString() ?? "-";

            var konfirmasi = MessageBox.Show(
                $"Yakin ingin menghapus bahan baku '{namaBahan}' secara permanen?",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{idBahan}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        $"Bahan baku '{namaBahan}' berhasil dihapus dari sistem.",
                        "Sukses",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await RefreshGridBahanBaku();
                }
                else
                {
                    MessageBox.Show(
                        "Gagal menghapus bahan baku.",
                        "Gagal",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshGridBahanBaku();
        }
    }
}
