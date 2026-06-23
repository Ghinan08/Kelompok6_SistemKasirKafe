using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace KasirKafe.Helpers
{
   
    public class TokoSettings
    {
        public string NamaToko { get; set; } = "KAFE KELOMPOK 6";
        public string NamaKasir { get; set; } = "Kasir";
        public string UcapanStruk { get; set; } = "Terima Kasih";
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }

    public class ApiHelper
    {
        private static readonly HttpClient _client = new HttpClient();
        private const string BaseUrl = "http://localhost:5011/api/";

        static ApiHelper()
        {
            _client.Timeout = TimeSpan.FromSeconds(10);
        }

        public static async Task<TokoSettings> GetInfoTokoAsync()
        {
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<TokoSettings>>($"{BaseUrl}Promo/info-toko");
                if (response != null && response.Success && response.Data != null)
                {
                    return response.Data;
                }
                throw new Exception(response?.Message ?? "Gagal mengambil info toko.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Koneksi API Error: {ex.Message}");
            }
        }

        public static async Task<string> GetCetakStrukAsync(int pesananId)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{BaseUrl}Pencetakan/{pesananId}/cetak");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                throw new Exception($"Gagal mencetak struk untuk ID Pesanan: {pesananId}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Koneksi API Error: {ex.Message}");
            }
        }
    }
}