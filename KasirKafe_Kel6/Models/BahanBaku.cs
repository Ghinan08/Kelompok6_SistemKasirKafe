using System;
using System.Text.Json.Serialization;

namespace KasirKafe_Kel6.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusBahanBaku
    {
        Habis,
        Menipis,
        TersediaBanyak,
        DiRestock
    }

    public class BahanBaku
    {
        public int Id { get; set; }
        public string NamaBahan { get; set; } = string.Empty;
        public int Stok { get;  set; }
        public StatusBahanBaku Status { get;  set; } = StatusBahanBaku.TersediaBanyak;

        protected BahanBaku() { }
        public BahanBaku(string namaBahan, int stok)
        {
            if (string.IsNullOrWhiteSpace(namaBahan))
                throw new ArgumentException("Nama bahan tidak boleh kosong.", nameof(namaBahan));

            if (stok < 0)
                throw new ArgumentException("Stok awal tidak boleh negatif.", nameof(stok));

            NamaBahan = namaBahan;
            Stok = stok;
            UpdateStatusAutomata();
        }

        public void UpdateStok(int jumlahPerubahan)
        {
            if (Stok + jumlahPerubahan < 0)
                throw new InvalidOperationException($"Stok tidak mencukupi! Sisa stok saat ini: {Stok}");

            Stok += jumlahPerubahan;

            if (jumlahPerubahan > 0 && Status == StatusBahanBaku.Habis)
                Status = StatusBahanBaku.DiRestock;
            else
                UpdateStatusAutomata();
        }

        public void UpdateStatusAutomata()
        {
            if (Stok == 0)
                Status = StatusBahanBaku.Habis;
            else if (Stok <= 20)
                Status = StatusBahanBaku.Menipis;
            else
                Status = StatusBahanBaku.TersediaBanyak;
        }
    }
}