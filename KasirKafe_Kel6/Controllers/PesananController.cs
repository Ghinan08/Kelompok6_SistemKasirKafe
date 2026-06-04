using KasirKafe_Kel6.DTO;
using KasirKafe_Kel6.Services;
using KasirKafe_Kel6.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace KasirKafe_Kel6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PesananController : ControllerBase
    {
        private readonly PesananService _service;

        public PesananController(PesananService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult TambahPesanan([FromBody] PesananRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var pesanan = _service.BuatPesanan(request);
                return Ok(ApiResponse<Pesanan>.Ok(pesanan, "Pesanan masuk antrean dapur. (Stok belum dipotong)"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<string>.Fail($"Gagal membuat pesanan: {ex.Message}"));
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _service.GetAllPesanan();
            return Ok(ApiResponse<IEnumerable<Pesanan>>.Ok(data, "Data pesanan berhasil diambil."));
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id)
        {
            try
            {
                var pesanan = _service.UpdateStatusPesanan(id);
                if (pesanan == null) return NotFound(ApiResponse<string>.Fail("Pesanan tidak ditemukan."));

                return Ok(ApiResponse<Pesanan>.Ok(pesanan, "Status pesanan berhasil diupdate."));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<string>.Fail(ex.Message));
            }
        }
    }
}