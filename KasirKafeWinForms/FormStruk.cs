using System;
using System.Windows.Forms;
using KasirKafe.Helpers;

namespace KasirKafe_Kel6
{
    public partial class FormStruk : Form
    {
        public FormStruk()
        {
            InitializeComponent();
        }

        private async void btnCariStruk_Click(object sender, EventArgs e)
        {
            // Secure Coding: Input Validation di sisi client
            if (!int.TryParse(txtPesananId.Text, out int pesananId) || pesananId <= 0)
            {
                MessageBox.Show("Silakan masukkan ID Pesanan yang valid (angka positif).",
                                "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
            
                // Memanggil fungsi API melalui kelas helper terpisah (SRP)
                string hasilStrukRaw = await ApiHelper.GetCetakStrukAsync(pesananId);

                // Secure Coding: Sensor data sensitif sebelum ditampilkan ke layar umum
                string hasilStrukAman = AmankanDataSensitif(hasilStrukRaw);

                rtxtDisplayStruk.Text = hasilStrukAman;
                lblStatusCetak.Text = "Status: Struk berhasil dimuat.";
            }
            catch (Exception ex)
            {
                // Safe Error Handling: Menampilkan pesan ramah tanpa bocoran sistem internal
                MessageBox.Show("Gagal memuat atau mencetak struk belanja. Pastikan ID pesanan valid dan berstatus Selesai.",
                                "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatusCetak.Text = "Status: Gagal memuat data.";
            }
        }

        private string AmankanDataSensitif(string? dataStruk)
        {
            if (string.IsNullOrEmpty(dataStruk)) return string.Empty;

            // Sensor format token transaksi payment gateway atau kode internal yang rahasia
            return dataStruk.Replace("DANA_", "DANA_XXXXX_")
                            .Replace("MIDTRANS_", "MDT_ID_******_")
                            .Replace("SECRET_", "SEC_***_");
        }

        private void rtxtDisplayStruk_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormStruk_Load(object sender, EventArgs e)
        {

        }
    }
}