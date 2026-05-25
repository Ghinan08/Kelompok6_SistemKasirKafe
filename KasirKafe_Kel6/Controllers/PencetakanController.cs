using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using KasirKafe_Kel6.Services;
using Microsoft.AspNetCore.Mvc;

namespace KasirKafe_Kel6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PencetakanController : ControllerBase
    {
        private readonly PencetakanService _cetakService;
        private readonly IRepository<Pesanan> _pesananRepo;

        public PencetakanController(PencetakanService cetakService, IRepository<Pesanan> pesananRepo)
        {
            _cetakService = cetakService;
            _pesananRepo = pesananRepo;
        }

        [HttpGet("{pesananId}/cetak")]
        public IActionResult CetakStruk(int pesananId)
        {
            var pesanan = _pesananRepo.GetById(pesananId);
            if (pesanan == null)
                return NotFound(ApiResponse<string>.Fail("Pesanan tidak ditemukan."));

            try
            {
                var hasilStruk = _cetakService.GenerateStruk(pesanan);
                return Ok(hasilStruk);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}