namespace KasirKafe_Kel6
{
    partial class FormBahanBaku
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader          = new Panel();
            lblJudul           = new Label();
            gbTambah           = new GroupBox();
            lblNamaBahan       = new Label();
            txtNamaBahan       = new TextBox();
            lblStokAwal        = new Label();
            nudStokAwal        = new NumericUpDown();
            btnTambahBahan     = new Button();
            gbUpdate           = new GroupBox();
            lblPerubahanStok   = new Label();
            nudJumlahPerubahan = new NumericUpDown();
            lblKeteranganStok  = new Label();
            btnUpdateStok      = new Button();
            btnHapusBahan      = new Button();
            pnlLegend          = new Panel();
            lblLegendJudul     = new Label();
            lblLegendHabis     = new Label();
            lblLegendMenipis   = new Label();
            lblLegendTersedia  = new Label();
            lblLegendRestock   = new Label();
            btnRefresh         = new Button();
            lblJumlahData      = new Label();
            dgvBahanBaku       = new DataGridView();

            pnlHeader.SuspendLayout();
            gbTambah.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudStokAwal).BeginInit();
            gbUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudJumlahPerubahan).BeginInit();
            pnlLegend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBahanBaku).BeginInit();
            SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(20, 20, 20);
            pnlHeader.Dock      = DockStyle.Top;
            pnlHeader.Height    = 55;
            pnlHeader.Controls.Add(lblJudul);

            // ── lblJudul ───────────────────────────────────────────
            lblJudul.AutoSize  = false;
            lblJudul.Dock      = DockStyle.Fill;
            lblJudul.Text      = "MANAJEMEN BAHAN BAKU — INVENTORI KASIR KAFE";
            lblJudul.Font      = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblJudul.ForeColor = Color.White;
            lblJudul.TextAlign = ContentAlignment.MiddleCenter;

            // ── gbTambah ───────────────────────────────────────────
            gbTambah.Text      = "Tambah Bahan Baku Baru";
            gbTambah.ForeColor = Color.White;
            gbTambah.Location  = new Point(20, 65);
            gbTambah.Size      = new Size(410, 155);
            gbTambah.Controls.AddRange(new Control[]
            {
                lblNamaBahan, txtNamaBahan,
                lblStokAwal,  nudStokAwal,
                btnTambahBahan
            });

            // ── lblNamaBahan ───────────────────────────────────────
            lblNamaBahan.Text      = "Nama Bahan:";
            lblNamaBahan.ForeColor = Color.White;
            lblNamaBahan.Location  = new Point(15, 28);
            lblNamaBahan.AutoSize  = true;

            // ── txtNamaBahan ───────────────────────────────────────
            txtNamaBahan.Location    = new Point(15, 48);
            txtNamaBahan.Size        = new Size(210, 23);
            txtNamaBahan.BackColor   = Color.FromArgb(50, 50, 50);
            txtNamaBahan.ForeColor   = Color.White;
            txtNamaBahan.BorderStyle = BorderStyle.FixedSingle;

            // ── lblStokAwal ────────────────────────────────────────
            lblStokAwal.Text      = "Stok Awal:";
            lblStokAwal.ForeColor = Color.White;
            lblStokAwal.Location  = new Point(245, 28);
            lblStokAwal.AutoSize  = true;

            // ── nudStokAwal ────────────────────────────────────────
            nudStokAwal.Location  = new Point(245, 48);
            nudStokAwal.Size      = new Size(100, 23);
            nudStokAwal.Minimum   = 0;
            nudStokAwal.Maximum   = 99999;
            nudStokAwal.Value     = 0;
            nudStokAwal.BackColor = Color.FromArgb(50, 50, 50);
            nudStokAwal.ForeColor = Color.White;

            // ── btnTambahBahan ─────────────────────────────────────
            btnTambahBahan.Text                           = "Tambah Bahan";
            btnTambahBahan.Location                       = new Point(15, 105);
            btnTambahBahan.Size                           = new Size(150, 32);
            btnTambahBahan.BackColor                      = Color.FromArgb(0, 100, 50);
            btnTambahBahan.ForeColor                      = Color.White;
            btnTambahBahan.FlatStyle                      = FlatStyle.Flat;
            btnTambahBahan.FlatAppearance.BorderColor     = Color.FromArgb(0, 150, 70);
            btnTambahBahan.Click                         += btnTambahBahan_Click;

            // ── gbUpdate ───────────────────────────────────────────
            gbUpdate.Text      = "Update / Hapus Stok  (pilih baris di tabel dahulu)";
            gbUpdate.ForeColor = Color.White;
            gbUpdate.Location  = new Point(450, 65);
            gbUpdate.Size      = new Size(450, 155);
            gbUpdate.Controls.AddRange(new Control[]
            {
                lblPerubahanStok, nudJumlahPerubahan,
                lblKeteranganStok,
                btnUpdateStok, btnHapusBahan
            });

            // ── lblPerubahanStok ───────────────────────────────────
            lblPerubahanStok.Text      = "Jumlah Perubahan Stok:";
            lblPerubahanStok.ForeColor = Color.White;
            lblPerubahanStok.Location  = new Point(15, 28);
            lblPerubahanStok.AutoSize  = true;

            // ── nudJumlahPerubahan ─────────────────────────────────
            nudJumlahPerubahan.Location  = new Point(15, 48);
            nudJumlahPerubahan.Size      = new Size(100, 23);
            nudJumlahPerubahan.Minimum   = -9999;
            nudJumlahPerubahan.Maximum   = 9999;
            nudJumlahPerubahan.Value     = 0;
            nudJumlahPerubahan.BackColor = Color.FromArgb(50, 50, 50);
            nudJumlahPerubahan.ForeColor = Color.White;

            // ── lblKeteranganStok ──────────────────────────────────
            lblKeteranganStok.Text      = "( + untuk tambah stok  /  - untuk kurangi stok )";
            lblKeteranganStok.ForeColor = Color.LightGray;
            lblKeteranganStok.Location  = new Point(125, 51);
            lblKeteranganStok.Size      = new Size(305, 18);
            lblKeteranganStok.Font      = new Font("Segoe UI", 8F, FontStyle.Italic);

            // ── btnUpdateStok ──────────────────────────────────────
            btnUpdateStok.Text                           = "Update Stok";
            btnUpdateStok.Location                       = new Point(15, 105);
            btnUpdateStok.Size                           = new Size(130, 32);
            btnUpdateStok.BackColor                      = Color.FromArgb(30, 80, 160);
            btnUpdateStok.ForeColor                      = Color.White;
            btnUpdateStok.FlatStyle                      = FlatStyle.Flat;
            btnUpdateStok.FlatAppearance.BorderColor     = Color.FromArgb(50, 110, 200);
            btnUpdateStok.Click                         += btnUpdateStok_Click;

            // ── btnHapusBahan ──────────────────────────────────────
            btnHapusBahan.Text                           = "Hapus Bahan";
            btnHapusBahan.Location                       = new Point(160, 105);
            btnHapusBahan.Size                           = new Size(130, 32);
            btnHapusBahan.BackColor                      = Color.FromArgb(140, 30, 30);
            btnHapusBahan.ForeColor                      = Color.White;
            btnHapusBahan.FlatStyle                      = FlatStyle.Flat;
            btnHapusBahan.FlatAppearance.BorderColor     = Color.FromArgb(200, 50, 50);
            btnHapusBahan.Click                         += btnHapusBahan_Click;

            // ── pnlLegend ──────────────────────────────────────────
            pnlLegend.BackColor = Color.FromArgb(25, 25, 25);
            pnlLegend.Location  = new Point(20, 230);
            pnlLegend.Size      = new Size(780, 26);
            pnlLegend.Controls.AddRange(new Control[]
            {
                lblLegendJudul,  lblLegendHabis,
                lblLegendMenipis, lblLegendTersedia, lblLegendRestock
            });

            // ── legend labels ──────────────────────────────────────
            lblLegendJudul.Text      = "Keterangan Warna:";
            lblLegendJudul.ForeColor = Color.LightGray;
            lblLegendJudul.Location  = new Point(5, 5);
            lblLegendJudul.AutoSize  = true;
            lblLegendJudul.Font      = new Font("Segoe UI", 8F, FontStyle.Bold);

            lblLegendHabis.Text      = "  ■ Habis  ";
            lblLegendHabis.ForeColor = Color.White;
            lblLegendHabis.BackColor = Color.FromArgb(139, 0, 0);
            lblLegendHabis.Location  = new Point(130, 4);
            lblLegendHabis.AutoSize  = true;

            lblLegendMenipis.Text      = "  ■ Menipis  ";
            lblLegendMenipis.ForeColor = Color.White;
            lblLegendMenipis.BackColor = Color.FromArgb(160, 90, 0);
            lblLegendMenipis.Location  = new Point(230, 4);
            lblLegendMenipis.AutoSize  = true;

            lblLegendTersedia.Text      = "  ■ Tersedia Banyak  ";
            lblLegendTersedia.ForeColor = Color.White;
            lblLegendTersedia.BackColor = Color.FromArgb(0, 90, 45);
            lblLegendTersedia.Location  = new Point(340, 4);
            lblLegendTersedia.AutoSize  = true;

            lblLegendRestock.Text      = "  ■ Di-Restock  ";
            lblLegendRestock.ForeColor = Color.White;
            lblLegendRestock.BackColor = Color.FromArgb(0, 80, 130);
            lblLegendRestock.Location  = new Point(495, 4);
            lblLegendRestock.AutoSize  = true;

            // ── btnRefresh ─────────────────────────────────────────
            btnRefresh.Text                           = "⟳ Refresh";
            btnRefresh.Location                       = new Point(820, 230);
            btnRefresh.Size                           = new Size(80, 26);
            btnRefresh.BackColor                      = Color.FromArgb(60, 60, 60);
            btnRefresh.ForeColor                      = Color.White;
            btnRefresh.FlatStyle                      = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderColor     = Color.Gray;
            btnRefresh.Click                         += btnRefresh_Click;

            // ── lblJumlahData ──────────────────────────────────────
            lblJumlahData.Text      = "Total: 0 bahan baku";
            lblJumlahData.ForeColor = Color.LightGray;
            lblJumlahData.Location  = new Point(20, 500);
            lblJumlahData.AutoSize  = true;
            lblJumlahData.Font      = new Font("Segoe UI", 8F, FontStyle.Italic);

            // ── dgvBahanBaku ───────────────────────────────────────
            dgvBahanBaku.Location                    = new Point(20, 262);
            dgvBahanBaku.Size                        = new Size(880, 230);
            dgvBahanBaku.BackgroundColor             = Color.FromArgb(30, 30, 30);
            dgvBahanBaku.BorderStyle                 = BorderStyle.None;
            dgvBahanBaku.SelectionMode               = DataGridViewSelectionMode.FullRowSelect;
            dgvBahanBaku.MultiSelect                 = false;
            dgvBahanBaku.RowHeadersVisible           = false;
            dgvBahanBaku.AllowUserToAddRows          = false;
            dgvBahanBaku.AllowUserToDeleteRows       = false;
            dgvBahanBaku.AutoSizeColumnsMode         = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBahanBaku.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgvBahanBaku.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(20, 20, 20);
            dgvBahanBaku.ColumnHeadersDefaultCellStyle.ForeColor  = Color.White;
            dgvBahanBaku.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvBahanBaku.DefaultCellStyle.BackColor                = Color.FromArgb(45, 45, 45);
            dgvBahanBaku.DefaultCellStyle.ForeColor                = Color.White;
            dgvBahanBaku.DefaultCellStyle.SelectionBackColor       = Color.FromArgb(0, 110, 200);
            dgvBahanBaku.DefaultCellStyle.SelectionForeColor       = Color.White;
            dgvBahanBaku.GridColor                                  = Color.FromArgb(60, 60, 60);

            // ── FormBahanBaku ──────────────────────────────────────
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            BackColor           = Color.FromArgb(30, 30, 30);
            ClientSize          = new Size(920, 520);
            Text                = "Manajemen Bahan Baku - Inventori";
            StartPosition       = FormStartPosition.CenterScreen;
            ForeColor           = Color.White;

            Controls.AddRange(new Control[]
            {
                pnlHeader,
                gbTambah,
                gbUpdate,
                pnlLegend,
                btnRefresh,
                dgvBahanBaku,
                lblJumlahData
            });

            Load += FormBahanBaku_Load;

            pnlHeader.ResumeLayout(false);
            gbTambah.ResumeLayout(false);
            gbTambah.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudStokAwal).EndInit();
            gbUpdate.ResumeLayout(false);
            gbUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudJumlahPerubahan).EndInit();
            pnlLegend.ResumeLayout(false);
            pnlLegend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBahanBaku).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel         pnlHeader;
        private Label         lblJudul;
        private GroupBox      gbTambah;
        private Label         lblNamaBahan;
        private TextBox       txtNamaBahan;
        private Label         lblStokAwal;
        private NumericUpDown nudStokAwal;
        private Button        btnTambahBahan;
        private GroupBox      gbUpdate;
        private Label         lblPerubahanStok;
        private NumericUpDown nudJumlahPerubahan;
        private Label         lblKeteranganStok;
        private Button        btnUpdateStok;
        private Button        btnHapusBahan;
        private Panel         pnlLegend;
        private Label         lblLegendJudul;
        private Label         lblLegendHabis;
        private Label         lblLegendMenipis;
        private Label         lblLegendTersedia;
        private Label         lblLegendRestock;
        private Button        btnRefresh;
        private Label         lblJumlahData;
        private DataGridView  dgvBahanBaku;
    }
}
