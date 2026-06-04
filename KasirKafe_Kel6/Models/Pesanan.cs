using System;
using System.Text.Json.Serialization;

namespace KasirKafe_Kel6.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusPesanan
    {
        Dipesan,
        Diproses,
        Selesai,
        Dibatalkan
    }

    public class Pesanan
    {
        public int Id { get; set; }
        public string NamaMenu { get; set; } = string.Empty;
        public int Jumlah { get; set; }
        public decimal TotalHarga { get; set; }
        public StatusPesanan Status { get; private set; } = StatusPesanan.Dipesan;

        public Pesanan() { }

        public Pesanan(string namaMenu, int jumlah, decimal totalHarga)
        {
            NamaMenu = namaMenu;
            Jumlah = jumlah;
            TotalHarga = totalHarga;
            Status = StatusPesanan.Dipesan;
        }

        public void ProsesPesanan()
        {
            if (Status == StatusPesanan.Dipesan)
                Status = StatusPesanan.Diproses;
            else
                throw new InvalidOperationException("Pesanan tidak dapat diproses dari status saat ini.");
        }

        public void SelesaikanPesanan()
        {
            if (Status == StatusPesanan.Diproses)
                Status = StatusPesanan.Selesai;
            else
                throw new InvalidOperationException("Pesanan tidak dapat diselesaikan dari status saat ini.");
        }

        public void BatalkanPesanan()
        {
            if (Status == StatusPesanan.Dipesan || Status == StatusPesanan.Diproses)
                Status = StatusPesanan.Dibatalkan;
            else
                throw new InvalidOperationException("Pesanan yang sudah selesai tidak dapat dibatalkan.");
        }
    }
}