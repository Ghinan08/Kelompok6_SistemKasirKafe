using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KasirKafe_Kel6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BahanBakuController : ControllerBase
    {
        private readonly IRepository<BahanBaku> _repo;

        public BahanBakuController(IRepository<BahanBaku> repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpPost]
        public IActionResult TambahBahanBaku([FromQuery] string nama, [FromQuery] int stokAwal)
        {
            try
            {
                var bahan = new BahanBaku(nama, stokAwal);
                _repo.Add(bahan);
                return Ok(ApiResponse<BahanBaku>.Ok(bahan, "Bahan baku berhasil ditambahkan"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpPut("{id}/update-stok")]
        public IActionResult UpdateStok(int id, [FromQuery] int perubahanStok)
        {
            var bahan = _repo.GetById(id);
            if (bahan == null) return NotFound(ApiResponse<string>.Fail("Bahan baku tidak ditemukan."));

            try
            {
                bahan.UpdateStok(perubahanStok);
                _repo.Update(bahan);
                return Ok(ApiResponse<BahanBaku>.Ok(bahan, "Stok dan status Automata berhasil diperbarui."));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}