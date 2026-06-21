using KasirKafe_Kel6.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KasirKafe_Kel6
{
    public partial class FormMenu : Form
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5011/api/Menu"; 

        public FormMenu()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void FormMenu_Load(object sender, EventArgs e)
        {
            await LoadDaftarMenuAsync();
            await LoadInfoDiskonAsync();
        }

        private async Task LoadDaftarMenuAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<ApiResponse<List<Models.Menu>>>(jsonString, options);
                    if (result != null && result.Success)
                    {
                        dgvDaftarMenu.DataSource = result.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data menu: {ex.Message}", "Error Jaringan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnTambahMenu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaMenu.Text))
            {
                MessageBox.Show("Peringatan Keamanan: Nama menu tidak boleh kosong atau hanya berisi spasi!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtHargaMenu.Text, out decimal hargaMenu))
            {
                MessageBox.Show("Peringatan Keamanan: Harga menu harus berupa angka valid!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (hargaMenu <= 0)
            {
                MessageBox.Show("Peringatan Keamanan: Harga menu harus lebih besar dari Rp0 dan tidak boleh negatif!", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var menuBaru = new Models.Menu
            {
                Nama = txtNamaMenu.Text.Trim(),
                Harga = hargaMenu
            };

            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(menuBaru), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(BaseUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Menu berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNamaMenu.Clear();
                    txtHargaMenu.Clear();
                    await LoadDaftarMenuAsync(); 
                }
                else
                {
                    MessageBox.Show("Gagal menambahkan menu. Periksa kembali aturan bisnis di server.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnHapusMenu_Click(object sender, EventArgs e)
        {
            if (dgvDaftarMenu.CurrentRow == null)
            {
                MessageBox.Show("Silakan pilih baris menu yang ingin dihapus pada tabel.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idMenu = Convert.ToInt32(dgvDaftarMenu.CurrentRow.Cells["Id"].Value);
            string namaMenu = dgvDaftarMenu.CurrentRow.Cells["Nama"].Value.ToString();

            var konfirmasi = MessageBox.Show($"Apakah Anda yakin ingin menghapus menu '{namaMenu}'?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.Yes)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync($"{BaseUrl}/{idMenu}");
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Menu berhasil dihapus dari sistem.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDaftarMenuAsync(); 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menghapus menu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task LoadInfoDiskonAsync()
        {
            try
            {
                string urlPromo = "http://localhost:5011/api/Promo/hitung?harga=100000";
                var response = await _httpClient.GetAsync(urlPromo);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(jsonString);
                    var root = doc.RootElement;

                    if (root.GetProperty("success").GetBoolean())
                    {
                        var data = root.GetProperty("data");
                        double persenDiskon = data.GetProperty("persenDiskon").GetDouble();
                        string namaHari = data.GetProperty("namaHari").GetString();

                        lblDiskonInfo.Text = $"Info Promo Hari ({namaHari}): Diskon Sebesar {persenDiskon * 100}%";
                    }
                }
            }
            catch
            {
                lblDiskonInfo.Text = "Gagal memuat informasi promo hari ini.";
            }
        }
    }
}