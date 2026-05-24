using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Services;
using Microsoft.AspNetCore.Mvc;

namespace KasirKafe_Kel6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KalkulasiHargaController : ControllerBase
    {
        private readonly KalkulasiHargaService _kalkulasiService;

        public KalkulasiHargaController(KalkulasiHargaService kalkulasiService)
        {
            _kalkulasiService = kalkulasiService
                ?? throw new ArgumentNullException(nameof(kalkulasiService));
        }

        [HttpGet("variasi")]
        public IActionResult GetDaftarVariasi()
        {
            var tabel = _kalkulasiService.GetTabelVariasi();
            return Ok(ApiResponse<Dictionary<string, decimal>>.Ok(
                tabel,
                $"Daftar variasi tersedia ({tabel.Count} item)"
            ));
        }

        [HttpPost("hitung")]
        public IActionResult HitungHarga([FromBody] KalkulasiHargaRequest request)
        {
            if (request == null)
                return BadRequest(ApiResponse<string>.Fail("Request body tidak boleh kosong."));

            if (request.MenuId <= 0)
                return BadRequest(ApiResponse<string>.Fail(
                    "menuId tidak valid. Harus berupa angka lebih besar dari 0."));

            try
            {
                var hasil = _kalkulasiService.HitungTotal(request.MenuId, request.Variasi ?? new());
                return Ok(ApiResponse<KalkulasiResult>.Ok(
                    hasil,
                    $"Kalkulasi harga untuk '{hasil.NamaMenu}' berhasil"
                ));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<string>.Fail(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}