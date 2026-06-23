using System.Text.Json.Serialization;

namespace KasirKafe_Kel6.Models
{
    /// <summary>
    /// Enum ini HARUS sama persis (nama & urutan) dengan StatusPesanan di backend,
    /// karena backend memakai [JsonConverter(typeof(JsonStringEnumConverter))]
    /// sehingga status dikirim sebagai string (contoh: "Dipesan", bukan angka 0).
    /// JsonStringEnumConverter di sini membuat deserialisasi mencocokkan
    /// nama string tersebut ke member enum yang sesuai.
    /// </summary>
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
        public StatusPesanan Status { get; set; }
    }
}