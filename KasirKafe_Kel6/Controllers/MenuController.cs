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
            return Ok(_menuRepo.GetAll());
        }

        [HttpPost]
        public IActionResult AddMenu([FromBody] Menu menuBaru)
        {
            if (string.IsNullOrWhiteSpace(menuBaru.Nama))
                return BadRequest("Nama menu tidak boleh kosong");

            if (menuBaru.Harga < 0)
                return BadRequest("Harga tidak boleh negatif");

            _menuRepo.Add(menuBaru);
            return Ok(new { message = "Menu berhasil ditambahkan", data = menuBaru });
        }
    }
}
