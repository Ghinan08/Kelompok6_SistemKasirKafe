using KasirKafe_Kel6.Models;
using Microsoft.Extensions.Options;

namespace KasirKafe_Kel6.Services
{
    public class PromoService
    {
        private readonly TokoSettings _toko;
        private readonly Dictionary<string, double> _diskonPerHari;

        public PromoService(IOptions<TokoSettings> tokoOptions, IConfiguration config)
        {
            _toko = tokoOptions.Value
                    ?? throw new ArgumentNullException(nameof(tokoOptions));

            _diskonPerHari = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            var section = config.GetSection("PromoSettings:DiskonPerHari");
            foreach (var item in section.GetChildren())
            {
                if (double.TryParse(item.Value,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double val))
                {
                    _diskonPerHari[item.Key] = val;
                }
            }
        }

        public HitungPromoResult HitungHargaAkhir(decimal hargaAwal, string? hariOverride = null)
        {
            if (hargaAwal < 0)
                throw new ArgumentException("Harga awal tidak boleh negatif.", nameof(hargaAwal));

            string hariIni = hariOverride ?? DateTime.Now.DayOfWeek.ToString();

            double persenDiskon = _diskonPerHari.TryGetValue(hariIni, out double d) ? d : 0.0;

            decimal diskon = hargaAwal * (decimal)persenDiskon;
            decimal setelahDiskon = hargaAwal - diskon;
            decimal pajak = setelahDiskon * (decimal)_toko.PajakPPN;
            decimal total = setelahDiskon + pajak;

            return new HitungPromoResult
            {
                HargaAwal = hargaAwal,
                NamaHari = hariIni,
                PersenDiskon = persenDiskon,
                NominalDiskon = diskon,
                Pajak = pajak,
                TotalAkhir = total,
                NamaKasir = _toko.NamaKasir,
                UcapanStruk = _toko.UcapanStruk
            };
        }
    }
}