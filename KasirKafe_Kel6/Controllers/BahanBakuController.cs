using KasirKafe_Kel6.DTO;
using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace KasirKafe_Kel6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BahanBakuController : ControllerBase
    {
        private readonly BahanBakuService _service;

        public BahanBakuController(BahanBakuService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult TambahBahanBaku([FromBody] BahanBakuRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var bahan = _service.TambahBahanBaru(request.NamaBahan, request.StokAwal);
                return Ok(ApiResponse<BahanBaku>.Ok(bahan, "Bahan baku berhasil ditambahkan."));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpPut("{id}/update-stok")]
        public IActionResult UpdateStok(int id, [FromQuery] int perubahanStok)
        {
            try
            {
                var bahan = _service.UpdateStokBahan(id, perubahanStok);
                if (bahan == null) return NotFound(ApiResponse<string>.Fail("Bahan baku tidak ditemukan."));

                return Ok(ApiResponse<BahanBaku>.Ok(bahan, "Stok dan status Automata berhasil diperbarui."));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _service.GetAllBahanBaku();
            return Ok(ApiResponse<IEnumerable<BahanBaku>>.Ok(data, "Data bahan baku berhasil diambil."));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var bahan = _service.GetBahanBakuById(id);
            if (bahan == null) return NotFound(ApiResponse<string>.Fail("Bahan baku tidak ditemukan."));

            return Ok(ApiResponse<BahanBaku>.Ok(bahan, "Data bahan baku ditemukan."));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isDeleted = _service.DeleteBahanBaku(id);
            if (!isDeleted) return NotFound(ApiResponse<string>.Fail("Bahan baku tidak ditemukan."));

            return Ok(ApiResponse<string>.Ok(string.Empty, "Bahan baku berhasil dihapus."));
        }
    }
}