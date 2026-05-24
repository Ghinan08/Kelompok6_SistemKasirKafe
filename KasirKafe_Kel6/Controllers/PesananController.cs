using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KasirKafe_Kel6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PesananController : ControllerBase
    {
        private readonly IRepository<Pesanan> _repository;

        public PesananController(IRepository<Pesanan> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult TambahPesanan(PesananRequest request)
        {
            var pesanan = new Pesanan
            {
                NamaMenu = request.NamaMenu,
                Jumlah = request.Jumlah,
                TotalHarga = request.TotalHarga,
                Status = "Dipesan"
            };

            _repository.Add(pesanan);

            return Ok(ApiResponse<Pesanan>.Ok(
                pesanan,
                "Pesanan berhasil dibuat"
            ));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _repository.GetAll();

            return Ok(ApiResponse<IEnumerable<Pesanan>>.Ok(
                data,
                "Data pesanan berhasil diambil"
            ));
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id)
        {
            var pesanan = _repository.GetById(id);

            if (pesanan == null)
            {
                return NotFound(ApiResponse<string>.Fail(
                    "Pesanan tidak ditemukan"
                ));
            }

            switch (pesanan.Status)
            {
                case "Dipesan":
                    pesanan.Status = "Diproses";
                    break;

                case "Diproses":
                    pesanan.Status = "Selesai";
                    break;

                case "Selesai":
                    return BadRequest(ApiResponse<string>.Fail(
                        "Pesanan sudah selesai"
                    ));
            }

            _repository.Update(pesanan);

            return Ok(ApiResponse<Pesanan>.Ok(
                pesanan,
                "Status pesanan berhasil diupdate"
            ));
        }
    }
}