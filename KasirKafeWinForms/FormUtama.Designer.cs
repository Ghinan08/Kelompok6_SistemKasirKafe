namespace KasirKafe_Kel6
{
    partial class FormUtama
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlSidebar = new Panel();
            btnNavStruk = new Button();
            btnNavBahanBaku = new Button();
            btnNavDapur = new Button();
            btnNavKasir = new Button();
            btnNavMenu = new Button();
            pnlHeader = new Panel();
            lblHeaderStatus = new Label();
            lblNamaToko = new Label();
            pnlMainContainer = new Panel();
            pnlSidebar.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(40, 40, 40);
            pnlSidebar.Controls.Add(btnNavMenu);
            pnlSidebar.Controls.Add(btnNavKasir);
            pnlSidebar.Controls.Add(btnNavDapur);
            pnlSidebar.Controls.Add(btnNavBahanBaku);
            pnlSidebar.Controls.Add(btnNavStruk);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(3, 4, 3, 4);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(229, 800);
            pnlSidebar.TabIndex = 0;
            // 
            // btnNavStruk
            // 
            btnNavStruk.ForeColor = SystemColors.ControlLightLight;
            btnNavStruk.Location = new Point(14, 333);
            btnNavStruk.Margin = new Padding(3, 4, 3, 4);
            btnNavStruk.Name = "btnNavStruk";
            btnNavStruk.Size = new Size(200, 53);
            btnNavStruk.TabIndex = 0;
            btnNavStruk.Text = "Cetak Struk";
            btnNavStruk.Click += btnNavStruk_Click;
            // 
            // btnNavBahanBaku
            // 
            btnNavBahanBaku.ForeColor = SystemColors.ControlLightLight;
            btnNavBahanBaku.Location = new Point(14, 260);
            btnNavBahanBaku.Margin = new Padding(3, 4, 3, 4);
            btnNavBahanBaku.Name = "btnNavBahanBaku";
            btnNavBahanBaku.Size = new Size(200, 53);
            btnNavBahanBaku.TabIndex = 1;
            btnNavBahanBaku.Text = "Inventory Bahan";
            btnNavBahanBaku.Click += btnNavBahanBaku_Click;
            // 
            // btnNavDapur
            // 
            btnNavDapur.ForeColor = SystemColors.ControlLightLight;
            btnNavDapur.Location = new Point(14, 187);
            btnNavDapur.Margin = new Padding(3, 4, 3, 4);
            btnNavDapur.Name = "btnNavDapur";
            btnNavDapur.Size = new Size(200, 53);
            btnNavDapur.TabIndex = 2;
            btnNavDapur.Text = "Antrean Dapur";
            btnNavDapur.Click += btnNavDapur_Click;
            // 
            // btnNavKasir
            // 
            btnNavKasir.ForeColor = SystemColors.ControlLightLight;
            btnNavKasir.Location = new Point(14, 113);
            btnNavKasir.Margin = new Padding(3, 4, 3, 4);
            btnNavKasir.Name = "btnNavKasir";
            btnNavKasir.Size = new Size(200, 53);
            btnNavKasir.TabIndex = 3;
            btnNavKasir.Text = "Kasir Utama";
            btnNavKasir.Click += btnNavKasir_Click;
            // 
            // btnNavMenu
            // 
            btnNavMenu.ForeColor = SystemColors.ControlLightLight;
            btnNavMenu.Location = new Point(14, 40);
            btnNavMenu.Margin = new Padding(3, 4, 3, 4);
            btnNavMenu.Name = "btnNavMenu";
            btnNavMenu.Size = new Size(200, 53);
            btnNavMenu.TabIndex = 4;
            btnNavMenu.Text = "Manajemen Menu";
            btnNavMenu.Click += btnNavMenu_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(50, 50, 50);
            pnlHeader.Controls.Add(lblHeaderStatus);
            pnlHeader.Controls.Add(lblNamaToko);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(229, 0);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(914, 107);
            pnlHeader.TabIndex = 1;
            // 
            // lblHeaderStatus
            // 
            lblHeaderStatus.ForeColor = Color.LightGray;
            lblHeaderStatus.Location = new Point(25, 64);
            lblHeaderStatus.Name = "lblHeaderStatus";
            lblHeaderStatus.Size = new Size(571, 27);
            lblHeaderStatus.TabIndex = 0;
            lblHeaderStatus.Text = "Menghubungkan ke API...";
            // 
            // lblNamaToko
            // 
            lblNamaToko.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblNamaToko.ForeColor = Color.White;
            lblNamaToko.Location = new Point(23, 20);
            lblNamaToko.Name = "lblNamaToko";
            lblNamaToko.Size = new Size(457, 40);
            lblNamaToko.TabIndex = 1;
            lblNamaToko.Text = "Memuat Nama Toko...";
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.BackColor = Color.FromArgb(30, 30, 30);
            pnlMainContainer.Dock = DockStyle.Fill;
            pnlMainContainer.Location = new Point(229, 107);
            pnlMainContainer.Margin = new Padding(3, 4, 3, 4);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Size = new Size(914, 693);
            pnlMainContainer.TabIndex = 2;
            // 
            // FormUtama
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 800);
            Controls.Add(pnlMainContainer);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormUtama";
            Text = "Sistem Kasir Kafe - Dashboard Utama";
            pnlSidebar.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Button btnNavStruk;
        private Button btnNavBahanBaku;
        private Button btnNavDapur;
        private Button btnNavKasir;
        private Button btnNavMenu;
        private Panel pnlHeader;
        private Label lblHeaderStatus;
        private Label lblNamaToko;
        private Panel pnlMainContainer; 
    }
}