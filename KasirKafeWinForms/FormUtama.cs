using KasirKafe.Helpers;
using KasirKafe_Kel6;
using System;
using System.Windows.Forms;

namespace KasirKafe_Kel6
{
    public partial class FormUtama : Form
    {
        private Form currentChildForm;

        public FormUtama()
        {
            InitializeComponent();
            LoadHeaderInfoToko();
        }

        // Memanggil API Info Toko pada Header Dashboard 
        private async void LoadHeaderInfoToko()
        {
            try
            {
                var infoToko = await ApiHelper.GetInfoTokoAsync();
                lblNamaToko.Text = infoToko.NamaToko.ToUpper();
                lblHeaderStatus.Text = $"Kasir Aktif: {infoToko.NamaKasir} | {infoToko.UcapanStruk}";
            }
            catch (Exception)
            {
                // Fallback default aman jika API bermasalah (Safe UI Experience)
                lblNamaToko.Text = "KAFE KELOMPOK 6";
                lblHeaderStatus.Text = "Sistem Offline - Mode Terbatas";
            }
        }

        // Fungsi Transisi Halaman (Panel Container System)
        public void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlMainContainer.Controls.Add(childForm);
            pnlMainContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // Event klik navigasi untuk memanggil form halaman anak (Integrasi Modul)
        private void btnNavMenu_Click(object sender, EventArgs e) => OpenChildForm(new FormMenu()); // Orang 1
        private void btnNavKasir_Click(object sender, EventArgs e) => OpenChildForm(new FormKasir()); // Orang 2
        private void btnNavDapur_Click(object sender, EventArgs e) => OpenChildForm(new FormDapur()); // Orang 3
        private void btnNavBahanBaku_Click(object sender, EventArgs e) => OpenChildForm(new FormBahanBaku()); // Orang 4
        private void btnNavStruk_Click(object sender, EventArgs e) => OpenChildForm(new FormStruk()); // Orang 5
    }
}