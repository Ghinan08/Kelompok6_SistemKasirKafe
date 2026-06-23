using KasirKafe_Kel6.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KasirKafe_Kel6
{
    public partial class FormDapur : Form
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5011/api/Pesanan";
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public FormDapur()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void FormDapur_Load(object sender, EventArgs e)
        {
            await LoadDaftarAntreanAsync();
        }

        private async Task LoadDaftarAntreanAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ApiResponse<List<Pesanan>>>(jsonString, _jsonOptions);

                    if (result != null && result.Success)
                    {
                        dgvAntrean.DataSource = result.Data;
                        FormatGridDapur();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Gagal memuat antrean dapur: {ex.Message}",
                    "Error Jaringan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatGridDapur()
        {
            if (dgvAntrean.Columns.Count == 0) return;

            dgvAntrean.ReadOnly = true;

            if (dgvAntrean.Columns.Contains("TotalHarga"))
            {
                dgvAntrean.Columns["TotalHarga"].DefaultCellStyle.Format = "N0";
                dgvAntrean.Columns["TotalHarga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            foreach (DataGridViewRow row in dgvAntrean.Rows)
            {
                if (row.Cells["Status"].Value == null) continue;

                string status = row.Cells["Status"].Value.ToString() ?? string.Empty;

                if (status == "Dipesan")
                    row.DefaultCellStyle.BackColor = Color.FromArgb(160, 90, 0);
                else if (status == "Diproses")
                    row.DefaultCellStyle.BackColor = Color.FromArgb(0, 90, 45);
                else
                    row.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);

                row.DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private async void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvAntrean.CurrentRow == null)
            {
                MessageBox.Show(
                    "Silakan pilih pesanan dari tabel antrean terlebih dahulu.",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int idPesanan = Convert.ToInt32(dgvAntrean.CurrentRow.Cells["Id"].Value);
            string namaMenu = dgvAntrean.CurrentRow.Cells["NamaMenu"].Value?.ToString() ?? "-";

            var konfirmasi = MessageBox.Show(
                $"Lanjutkan proses pesanan '{namaMenu}' ke tahap berikutnya?",
                "Konfirmasi Update Status",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                var response = await _httpClient.PutAsync($"{BaseUrl}/{idPesanan}/status", null);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        $"Status pesanan '{namaMenu}' berhasil diperbarui ke tahap selanjutnya!",
                        "Sukses",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadDaftarAntreanAsync();
                }
                else
                {
                    var errorJson = await response.Content.ReadAsStringAsync();
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<string>>(errorJson, _jsonOptions);
                    MessageBox.Show(
                        errorResult?.Message ?? "Gagal mengupdate status.",
                        "Penolakan Sistem",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
            await LoadDaftarAntreanAsync();
        }
    }
}