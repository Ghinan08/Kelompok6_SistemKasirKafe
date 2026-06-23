using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace KasirKafe_Kel6
{
    partial class FormDapur
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
            pnlHeader = new System.Windows.Forms.Panel();
            lblJudul = new System.Windows.Forms.Label();
            dgvAntrean = new System.Windows.Forms.DataGridView();
            btnUpdateStatus = new System.Windows.Forms.Button();
            btnRefresh = new System.Windows.Forms.Button();
            lblPetunjuk = new System.Windows.Forms.Label();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAntrean).BeginInit();
            SuspendLayout();

            pnlHeader.BackColor = System.Drawing.Color.FromArgb(20, 20, 20);
            pnlHeader.Controls.Add(lblJudul);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(800, 60);
            pnlHeader.TabIndex = 0;

            lblJudul.Dock = System.Windows.Forms.DockStyle.Fill;
            lblJudul.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblJudul.ForeColor = System.Drawing.Color.White;
            lblJudul.Location = new System.Drawing.Point(0, 0);
            lblJudul.Name = "lblJudul";
            lblJudul.Size = new System.Drawing.Size(800, 60);
            lblJudul.TabIndex = 0;
            lblJudul.Text = "ANTREAN DAPUR";
            lblJudul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            dgvAntrean.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvAntrean.BackgroundColor = System.Drawing.Color.FromArgb(40, 40, 40);
            dgvAntrean.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvAntrean.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAntrean.Location = new System.Drawing.Point(20, 110);
            dgvAntrean.MultiSelect = false;
            dgvAntrean.Name = "dgvAntrean";
            dgvAntrean.ReadOnly = true;
            dgvAntrean.RowHeadersVisible = false;
            dgvAntrean.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvAntrean.Size = new System.Drawing.Size(745, 270);
            dgvAntrean.TabIndex = 1;

            btnUpdateStatus.BackColor = System.Drawing.Color.Indigo;
            btnUpdateStatus.FlatAppearance.BorderSize = 0;
            btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnUpdateStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            btnUpdateStatus.Location = new System.Drawing.Point(20, 400);
            btnUpdateStatus.Name = "btnUpdateStatus";
            btnUpdateStatus.Size = new System.Drawing.Size(200, 40);
            btnUpdateStatus.TabIndex = 2;
            btnUpdateStatus.Text = "Proses / Selesaikan";
            btnUpdateStatus.UseVisualStyleBackColor = false;
            btnUpdateStatus.Click += btnUpdateStatus_Click;

            btnRefresh.BackColor = System.Drawing.Color.FromArgb(60, 60, 60);
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Location = new System.Drawing.Point(645, 400);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(120, 40);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh Data";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;

            lblPetunjuk.AutoSize = true;
            lblPetunjuk.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            lblPetunjuk.ForeColor = System.Drawing.Color.LightGray;
            lblPetunjuk.Location = new System.Drawing.Point(20, 80);
            lblPetunjuk.Name = "lblPetunjuk";
            lblPetunjuk.Size = new System.Drawing.Size(434, 15);
            lblPetunjuk.TabIndex = 4;
            lblPetunjuk.Text = "Pilih pesanan dari tabel, lalu tekan tombol Proses untuk mengubah status antrean.";

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(800, 470);
            Controls.Add(lblPetunjuk);
            Controls.Add(btnRefresh);
            Controls.Add(btnUpdateStatus);
            Controls.Add(dgvAntrean);
            Controls.Add(pnlHeader);
            Name = "FormDapur";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Modul Dapur";
            Load += FormDapur_Load;
            pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAntrean).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.DataGridView dgvAntrean;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblPetunjuk;
    }
}