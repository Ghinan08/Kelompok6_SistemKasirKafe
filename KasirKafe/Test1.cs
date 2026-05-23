using KasirKafe_Kel6.Data;
using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KasirKafe
{
    [TestClass]
    public class RepositoryTests
    {
        private AppDbContext _context;
        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_KasirKafeDb_" + System.Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
        }

        [TestMethod]
        public void Test_Repository_AddAndGetAll_Success()
        {
            var repository = new Repository<Menu>(_context);
            var menuBaru = new Menu { Nama = "Es Kopi Susu", Harga = 15000 };

            repository.Add(menuBaru);
            var result = repository.GetAll();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Es Kopi Susu", result.First().Nama);
        }
    }
}
