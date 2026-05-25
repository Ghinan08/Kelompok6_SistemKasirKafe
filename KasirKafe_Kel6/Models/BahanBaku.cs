namespace KasirKafe_Kel6.Models
{
    public class BahanBaku
    {
        public int Id { get; set; }
        public string NamaBahan { get; set; } = string.Empty;
        public int Stok { get; private set; }
        public string Status { get; private set; } = "Tersedia Banyak";

        public BahanBaku(string namaBahan, int stok)
        {
            if (string.IsNullOrWhiteSpace(namaBahan))
                throw new ArgumentException("Nama bahan tidak boleh kosong.", nameof(namaBahan));

            if (stok < 0)
                throw new ArgumentException("Stok tidak boleh negatif.", nameof(stok));

            NamaBahan = namaBahan;
            Stok = stok;
            UpdateStatusAutomata();
        }

        public void UpdateStok(int jumlahPerubahan)
        {
            if (Stok + jumlahPerubahan < 0)
                throw new InvalidOperationException($"Stok tidak mencukupi! Sisa stok saat ini: {Stok}");

            Stok += jumlahPerubahan;

            if (jumlahPerubahan > 0 && Status == "Habis")
            {
                Status = "Di-restock";
            }
            else
            {
                UpdateStatusAutomata();
            }
        }

        public void UpdateStatusAutomata()
        {
            if (Stok == 0)
                Status = "Habis";
            else if (Stok <= 20)
                Status = "Menipis";
            else
                Status = "Tersedia Banyak";
        }
    }
}