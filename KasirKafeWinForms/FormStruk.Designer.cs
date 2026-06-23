namespace KasirKafe_Kel6
{
    partial class FormStruk
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
            txtPesananId = new TextBox();
            btnCariStruk = new Button();
            rtxtDisplayStruk = new RichTextBox();
            lblStatusCetak = new Label();
            lblJudulHalaman = new Label();
            SuspendLayout();
            // 
            // txtPesananId
            // 
            txtPesananId.Location = new Point(29, 93);
            txtPesananId.Margin = new Padding(3, 4, 3, 4);
            txtPesananId.Name = "txtPesananId";
            txtPesananId.PlaceholderText = "Masukkan ID Pesanan...";
            txtPesananId.Size = new Size(205, 27);
            txtPesananId.TabIndex = 3;
            // 
            // btnCariStruk
            // 
            btnCariStruk.ForeColor = SystemColors.ControlLightLight;
            btnCariStruk.Location = new Point(251, 92);
            btnCariStruk.Margin = new Padding(3, 4, 3, 4);
            btnCariStruk.Name = "btnCariStruk";
            btnCariStruk.Size = new Size(126, 33);
            btnCariStruk.TabIndex = 2;
            btnCariStruk.Text = "Cetak Struk";
            btnCariStruk.Click += btnCariStruk_Click;
            // 
            // rtxtDisplayStruk
            // 
            rtxtDisplayStruk.BackColor = Color.Black;
            rtxtDisplayStruk.Font = new Font("Courier New", 10F);
            rtxtDisplayStruk.ForeColor = Color.Lime;
            rtxtDisplayStruk.Location = new Point(29, 180);
            rtxtDisplayStruk.Margin = new Padding(3, 4, 3, 4);
            rtxtDisplayStruk.Name = "rtxtDisplayStruk";
            rtxtDisplayStruk.ReadOnly = true;
            rtxtDisplayStruk.Size = new Size(499, 452);
            rtxtDisplayStruk.TabIndex = 0;
            rtxtDisplayStruk.Text = "";
            rtxtDisplayStruk.TextChanged += rtxtDisplayStruk_TextChanged;
            // 
            // lblStatusCetak
            // 
            lblStatusCetak.ForeColor = Color.Yellow;
            lblStatusCetak.Location = new Point(29, 140);
            lblStatusCetak.Name = "lblStatusCetak";
            lblStatusCetak.Size = new Size(343, 27);
            lblStatusCetak.TabIndex = 1;
            lblStatusCetak.Text = "Status: Siap.";
            // 
            // lblJudulHalaman
            // 
            lblJudulHalaman.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblJudulHalaman.ForeColor = Color.White;
            lblJudulHalaman.Location = new Point(23, 27);
            lblJudulHalaman.Name = "lblJudulHalaman";
            lblJudulHalaman.Size = new Size(343, 40);
            lblJudulHalaman.TabIndex = 4;
            lblJudulHalaman.Text = "MODUL CETAK STRUK";
            // 
            // FormStruk
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(805, 667);
            Controls.Add(rtxtDisplayStruk);
            Controls.Add(lblStatusCetak);
            Controls.Add(btnCariStruk);
            Controls.Add(txtPesananId);
            Controls.Add(lblJudulHalaman);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormStruk";
            Text = "Cetak Struk Pembayaran";
            Load += FormStruk_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPesananId;
        private Button btnCariStruk;
        private RichTextBox rtxtDisplayStruk;
        private Label lblStatusCetak;
        private Label lblJudulHalaman;
    }
}
