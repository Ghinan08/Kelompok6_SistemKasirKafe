namespace KasirKafe_Kel6
{
    partial class FormMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            btnTambahMenu = new Button();
            lblDiskonInfo = new Label();
            txtNamaMenu = new TextBox();
            dgvDaftarMenu = new DataGridView();
            txtHargaMenu = new TextBox();
            btnHapusMenu = new Button();
            lblNamaMenu = new Label();
            lblHarga = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvDaftarMenu).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnTambahMenu
            // 
            btnTambahMenu.BackColor = Color.Indigo;
            btnTambahMenu.FlatStyle = FlatStyle.Flat;
            btnTambahMenu.Location = new Point(26, 110);
            btnTambahMenu.Name = "btnTambahMenu";
            btnTambahMenu.Size = new Size(132, 23);
            btnTambahMenu.TabIndex = 0;
            btnTambahMenu.Text = "Tambah Menu";
            btnTambahMenu.UseVisualStyleBackColor = false;
            btnTambahMenu.Click += btnTambahMenu_Click;
            // 
            // lblDiskonInfo
            // 
            lblDiskonInfo.AutoSize = true;
            lblDiskonInfo.ForeColor = Color.Crimson;
            lblDiskonInfo.Location = new Point(246, 215);
            lblDiskonInfo.Name = "lblDiskonInfo";
            lblDiskonInfo.Size = new Size(123, 15);
            lblDiskonInfo.TabIndex = 1;
            lblDiskonInfo.Text = "Memuat info diskon...";
            // 
            // txtNamaMenu
            // 
            txtNamaMenu.Location = new Point(26, 52);
            txtNamaMenu.Name = "txtNamaMenu";
            txtNamaMenu.Size = new Size(100, 23);
            txtNamaMenu.TabIndex = 2;
            // 
            // dgvDaftarMenu
            // 
            dgvDaftarMenu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDaftarMenu.BackgroundColor = SystemColors.ControlDarkDark;
            dgvDaftarMenu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.MenuText;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvDaftarMenu.DefaultCellStyle = dataGridViewCellStyle1;
            dgvDaftarMenu.Location = new Point(246, 245);
            dgvDaftarMenu.Name = "dgvDaftarMenu";
            dgvDaftarMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDaftarMenu.Size = new Size(319, 158);
            dgvDaftarMenu.TabIndex = 3;
            // 
            // txtHargaMenu
            // 
            txtHargaMenu.Location = new Point(197, 52);
            txtHargaMenu.Name = "txtHargaMenu";
            txtHargaMenu.Size = new Size(100, 23);
            txtHargaMenu.TabIndex = 4;
            // 
            // btnHapusMenu
            // 
            btnHapusMenu.BackColor = Color.Indigo;
            btnHapusMenu.FlatStyle = FlatStyle.Flat;
            btnHapusMenu.Location = new Point(168, 110);
            btnHapusMenu.Name = "btnHapusMenu";
            btnHapusMenu.Size = new Size(129, 23);
            btnHapusMenu.TabIndex = 5;
            btnHapusMenu.Text = "Hapus Menu";
            btnHapusMenu.UseVisualStyleBackColor = false;
            btnHapusMenu.Click += btnHapusMenu_Click;
            // 
            // lblNamaMenu
            // 
            lblNamaMenu.AutoSize = true;
            lblNamaMenu.Location = new Point(26, 34);
            lblNamaMenu.Name = "lblNamaMenu";
            lblNamaMenu.Size = new Size(76, 15);
            lblNamaMenu.TabIndex = 6;
            lblNamaMenu.Text = "Nama Menu:";
            // 
            // lblHarga
            // 
            lblHarga.AutoSize = true;
            lblHarga.Location = new Point(197, 34);
            lblHarga.Name = "lblHarga";
            lblHarga.Size = new Size(42, 15);
            lblHarga.TabIndex = 7;
            lblHarga.Text = "Harga:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtNamaMenu);
            groupBox1.Controls.Add(btnHapusMenu);
            groupBox1.Controls.Add(lblHarga);
            groupBox1.Controls.Add(lblNamaMenu);
            groupBox1.Controls.Add(txtHargaMenu);
            groupBox1.Controls.Add(btnTambahMenu);
            groupBox1.ForeColor = Color.WhiteSmoke;
            groupBox1.Location = new Point(246, 35);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(319, 164);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Form Input Menu";
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(dgvDaftarMenu);
            Controls.Add(lblDiskonInfo);
            ForeColor = Color.White;
            Name = "FormMenu";
            Text = "Form1";
            Load += FormMenu_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDaftarMenu).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTambahMenu;
        private Label lblDiskonInfo;
        private TextBox txtNamaMenu;
        private DataGridView dgvDaftarMenu;
        private TextBox txtHargaMenu;
        private Button btnHapusMenu;
        private Label lblNamaMenu;
        private Label lblHarga;
        private GroupBox groupBox1;
    }
}
