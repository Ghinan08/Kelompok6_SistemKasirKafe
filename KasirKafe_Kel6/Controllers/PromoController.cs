using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KasirKafe_Kel6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromoController : ControllerBase
    {
        private readonly PromoService _promoService;

        public PromoController(PromoService promoService)
        {
            _promoService = promoService
                ?? throw new ArgumentNullException(nameof(promoService));
        }

        [HttpGet("hitung")]
        public IActionResult HitungPromo([FromQuery] decimal harga)
        {
            if (harga <= 0)
                return BadRequest(ApiResponse<string>.Fail("Harga harus lebih dari 0."));

            try
            {
                var result = _promoService.HitungHargaAkhir(harga);
                return Ok(ApiResponse<HitungPromoResult>.Ok(result, "Kalkulasi promo berhasil."));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet("info-toko")]
        public IActionResult InfoToko([FromServices] IOptions<TokoSettings> toko)
        {
            return Ok(ApiResponse<TokoSettings>.Ok(toko.Value, "Info toko berhasil diambil."));
        }
    }
}