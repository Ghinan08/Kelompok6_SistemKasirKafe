namespace KasirKafe_Kel6
{
    partial class FormKasir
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private ComboBox cmbMenu;
        private Label lblMenu;

        private Label lblJumlahPorsi;
        private NumericUpDown nudJumlahPorsi;

        private CheckedListBox clbVariasi;
        private Label lblVariasi;

        // Panel ini diisi NumericUpDown jumlah per variasi secara dinamis saat
        // runtime (lihat FormKasir.cs), karena daftar variasi datang dari API
        // dan tidak diketahui di waktu desain.
        private Panel pnlVariasiJumlah;

        private ListView lvRincian;

        private Label lblHargaDasar;
        private Label lblHargaDasarValue;
        private Label lblTotalTambahan;
        private Label lblTotalTambahanValue;
        private Label lblTotalAkhir;
        private Label lblTotalAkhirValue;

        private Button btnHitungSubtotal;
        private Button btnKirimPesanan;

        private void InitializeComponent()
        {
            cmbMenu = new ComboBox();
            lblMenu = new Label();
            lblJumlahPorsi = new Label();
            nudJumlahPorsi = new NumericUpDown();
            clbVariasi = new CheckedListBox();
            lblVariasi = new Label();
            pnlVariasiJumlah = new Panel();
            lvRincian = new ListView();
            lblHargaDasar = new Label();
            lblHargaDasarValue = new Label();
            lblTotalTambahan = new Label();
            lblTotalTambahanValue = new Label();
            lblTotalAkhir = new Label();
            lblTotalAkhirValue = new Label();
            btnHitungSubtotal = new Button();
            btnKirimPesanan = new Button();

            ((System.ComponentModel.ISupportInitialize)nudJumlahPorsi).BeginInit();
            SuspendLayout();

            // --- lblMenu ---
            lblMenu.AutoSize = true;
            lblMenu.Location = new Point(20, 20);
            lblMenu.Name = "lblMenu";
            lblMenu.Text = "Pilih Menu:";

            // --- cmbMenu ---
            cmbMenu.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMenu.Location = new Point(20, 45);
            cmbMenu.Name = "cmbMenu";
            cmbMenu.Size = new Size(300, 28);
            cmbMenu.SelectedIndexChanged += cmbMenu_SelectedIndexChanged;

            // --- lblJumlahPorsi ---
            lblJumlahPorsi.AutoSize = true;
            lblJumlahPorsi.Location = new Point(340, 20);
            lblJumlahPorsi.Name = "lblJumlahPorsi";
            lblJumlahPorsi.Text = "Jumlah Porsi:";

            // --- nudJumlahPorsi ---
            // SECURE CODING: dibatasi 1 s/d 99 untuk mencegah integer overflow
            // saat backend mengalikan TotalAkhir per-porsi * Jumlah.
            nudJumlahPorsi.Location = new Point(340, 45);
            nudJumlahPorsi.Name = "nudJumlahPorsi";
            nudJumlahPorsi.Size = new Size(80, 27);
            nudJumlahPorsi.Minimum = 1;
            nudJumlahPorsi.Maximum = 99;
            nudJumlahPorsi.Value = 1;

            // --- lblVariasi ---
            lblVariasi.AutoSize = true;
            lblVariasi.Location = new Point(20, 85);
            lblVariasi.Name = "lblVariasi";
            lblVariasi.Text = "Variasi Tambahan (centang lalu atur jumlah):";

            // --- clbVariasi ---
            clbVariasi.Location = new Point(20, 110);
            clbVariasi.Name = "clbVariasi";
            clbVariasi.Size = new Size(260, 180);
            clbVariasi.CheckOnClick = true;
            clbVariasi.ItemCheck += clbVariasi_ItemCheck;

            // --- pnlVariasiJumlah ---
            pnlVariasiJumlah.Location = new Point(300, 110);
            pnlVariasiJumlah.Name = "pnlVariasiJumlah";
            pnlVariasiJumlah.Size = new Size(220, 180);
            pnlVariasiJumlah.AutoScroll = true;
            pnlVariasiJumlah.BorderStyle = BorderStyle.FixedSingle;

            // --- lvRincian ---
            lvRincian.Location = new Point(20, 310);
            lvRincian.Name = "lvRincian";
            lvRincian.Size = new Size(500, 150);
            lvRincian.View = View.Details;
            lvRincian.FullRowSelect = true;
            lvRincian.GridLines = true;
            lvRincian.Columns.Add("Variasi", 180);
            lvRincian.Columns.Add("Jumlah", 80);
            lvRincian.Columns.Add("Harga Satuan", 120);
            lvRincian.Columns.Add("Subtotal", 120);

            // --- lblHargaDasar ---
            lblHargaDasar.AutoSize = true;
            lblHargaDasar.Location = new Point(20, 475);
            lblHargaDasar.Name = "lblHargaDasar";
            lblHargaDasar.Text = "Harga Dasar:";

            lblHargaDasarValue.AutoSize = true;
            lblHargaDasarValue.Location = new Point(160, 475);
            lblHargaDasarValue.Name = "lblHargaDasarValue";
            lblHargaDasarValue.Text = "Rp 0";

            // --- lblTotalTambahan ---
            lblTotalTambahan.AutoSize = true;
            lblTotalTambahan.Location = new Point(20, 500);
            lblTotalTambahan.Name = "lblTotalTambahan";
            lblTotalTambahan.Text = "Total Tambahan:";

            lblTotalTambahanValue.AutoSize = true;
            lblTotalTambahanValue.Location = new Point(160, 500);
            lblTotalTambahanValue.Name = "lblTotalTambahanValue";
            lblTotalTambahanValue.Text = "Rp 0";

            // --- lblTotalAkhir ---
            lblTotalAkhir.AutoSize = true;
            lblTotalAkhir.Font = new Font(Font, FontStyle.Bold);
            lblTotalAkhir.Location = new Point(20, 530);
            lblTotalAkhir.Name = "lblTotalAkhir";
            lblTotalAkhir.Text = "TOTAL AKHIR:";

            lblTotalAkhirValue.AutoSize = true;
            lblTotalAkhirValue.Font = new Font(Font, FontStyle.Bold);
            lblTotalAkhirValue.Location = new Point(160, 530);
            lblTotalAkhirValue.Name = "lblTotalAkhirValue";
            lblTotalAkhirValue.Text = "Rp 0";

            // --- btnHitungSubtotal ---
            btnHitungSubtotal.BackColor = Color.Indigo;
            btnHitungSubtotal.FlatStyle = FlatStyle.Flat;
            btnHitungSubtotal.Location = new Point(380, 470);
            btnHitungSubtotal.Name = "btnHitungSubtotal";
            btnHitungSubtotal.Size = new Size(160, 30);
            btnHitungSubtotal.Text = "Hitung Subtotal";
            btnHitungSubtotal.UseVisualStyleBackColor = false;
            btnHitungSubtotal.Click += btnHitungSubtotal_Click;

            // --- btnKirimPesanan ---
            btnKirimPesanan.BackColor = Color.Indigo;
            btnKirimPesanan.FlatStyle = FlatStyle.Flat;
            btnKirimPesanan.Location = new Point(380, 510);
            btnKirimPesanan.Name = "btnKirimPesanan";
            btnKirimPesanan.Size = new Size(160, 30);
            btnKirimPesanan.Text = "Kirim Pesanan";
            btnKirimPesanan.UseVisualStyleBackColor = false;
            btnKirimPesanan.Enabled = false;
            btnKirimPesanan.Click += btnKirimPesanan_Click;

            // --- FormKasir ---
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;
            ClientSize = new Size(560, 580);
            Name = "FormKasir";
            Text = "Kasir - Input Pesanan";
            Controls.Add(lblMenu);
            Controls.Add(cmbMenu);
            Controls.Add(lblJumlahPorsi);
            Controls.Add(nudJumlahPorsi);
            Controls.Add(lblVariasi);
            Controls.Add(clbVariasi);
            Controls.Add(pnlVariasiJumlah);
            Controls.Add(lvRincian);
            Controls.Add(lblHargaDasar);
            Controls.Add(lblHargaDasarValue);
            Controls.Add(lblTotalTambahan);
            Controls.Add(lblTotalTambahanValue);
            Controls.Add(lblTotalAkhir);
            Controls.Add(lblTotalAkhirValue);
            Controls.Add(btnHitungSubtotal);
            Controls.Add(btnKirimPesanan);
            Load += FormKasir_Load;

            ((System.ComponentModel.ISupportInitialize)nudJumlahPorsi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}