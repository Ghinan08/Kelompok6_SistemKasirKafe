using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KasirKafe_Kel6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IRepository<Menu> _menuRepo;

        public MenuController(IRepository<Menu> menuRepo)
        {
            _menuRepo = menuRepo;
        }

        [HttpGet]
        public IActionResult GetAllMenu()
        {
            var data = _menuRepo.GetAll().ToList();
            return Ok(ApiResponse<List<Menu>>.Ok(data, $"Berhasil mengambil {data.Count} menu"));
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuById(int id)
        {
            if (id <= 0)
                return BadRequest(ApiResponse<string>.Fail("ID menu harus lebih besar dari 0."));

            var menu = _menuRepo.GetById(id);
            if (menu == null)
                return NotFound(ApiResponse<string>.Fail($"Menu dengan ID {id} tidak ditemukan."));

            return Ok(ApiResponse<Menu>.Ok(menu, "Menu ditemukan"));
        }

        [HttpPost]
        public IActionResult AddMenu([FromBody] Menu menuBaru)
        {
            if (string.IsNullOrWhiteSpace(menuBaru.Nama))
                return BadRequest(ApiResponse<string>.Fail("Nama menu tidak boleh kosong."));

            if (menuBaru.Harga < 0)
                return BadRequest(ApiResponse<string>.Fail("Harga tidak boleh negatif."));

            if (menuBaru.Harga == 0)
                return BadRequest(ApiResponse<string>.Fail("Harga menu tidak boleh nol."));

            _menuRepo.Add(menuBaru);
            return Ok(ApiResponse<Menu>.Ok(menuBaru, "Menu berhasil ditambahkan"));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(int id)
        {
            if (id <= 0)
                return BadRequest(ApiResponse<string>.Fail("ID menu harus lebih besar dari 0."));

            var menu = _menuRepo.GetById(id);
            if (menu == null)
                return NotFound(ApiResponse<string>.Fail($"Menu dengan ID {id} tidak ditemukan."));

            _menuRepo.Delete(id);
            return Ok(ApiResponse<string>.Ok($"Menu '{menu.Nama}'", "Menu berhasil dihapus"));
        }
    }
}