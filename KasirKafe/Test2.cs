using KasirKafe_Kel6.Data;
using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using KasirKafe_Kel6.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KasirKafe
{
    public static class AssertHelper
    {
        public static void Throws<T>(Action action) where T : Exception
        {
            try
            {
                action();
                Assert.Fail($"Diharapkan melempar exception tipe {typeof(T).Name}, tetapi tidak ada error.");
            }
            catch (T)
            {
            }
        }
    }

    [TestClass]
    public class ApiResponseTests
    {
        [TestMethod]
        public void ApiResponse_Ok_ShouldReturnSuccessTrue()
        {
            var response = ApiResponse<string>.Ok("data test");
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public void ApiResponse_Ok_ShouldContainCorrectData()
        {
            var menu = new Menu { Id = 1, Nama = "Kopi Susu", Harga = 20000 };
            var response = ApiResponse<Menu>.Ok(menu);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual("Kopi Susu", response.Data.Nama);
        }

        [TestMethod]
        public void ApiResponse_Ok_WithCustomMessage_ShouldUseMessage()
        {
            var response = ApiResponse<string>.Ok("ok", "Menu berhasil ditambahkan");
            Assert.AreEqual("Menu berhasil ditambahkan", response.Message);
        }

        [TestMethod]
        public void ApiResponse_Ok_WithListData_ShouldWork()
        {
            var list = new List<Menu>
            {
                new Menu { Id = 1, Nama = "Es Teh", Harga = 5000 },
                new Menu { Id = 2, Nama = "Kopi Hitam", Harga = 10000 }
            };
            var response = ApiResponse<List<Menu>>.Ok(list, "2 menu ditemukan");
            Assert.IsTrue(response.Success);

            int jumlahData = response.Data!.Count;
            Assert.AreEqual(2, jumlahData);
        }

        [TestMethod]
        public void ApiResponse_Ok_WithNullData_ShouldThrow()
        {
            AssertHelper.Throws<ArgumentNullException>(() => { var _ = ApiResponse<Menu>.Ok(null!); });
        }

        [TestMethod]
        public void ApiResponse_Ok_WithEmptyMessage_ShouldThrow()
        {
            AssertHelper.Throws<ArgumentException>(() => { var _ = ApiResponse<string>.Ok("data", "   "); });
        }

        [TestMethod]
        public void ApiResponse_Fail_ShouldReturnSuccessFalse()
        {
            var response = ApiResponse<string>.Fail("Terjadi kesalahan");
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public void ApiResponse_Fail_DataShouldBeNull()
        {
            var response = ApiResponse<Menu>.Fail("Error");
            Assert.IsNull(response.Data);
        }

        [TestMethod]
        public void ApiResponse_Fail_WithEmptyMessage_ShouldThrow()
        {
            AssertHelper.Throws<ArgumentException>(() => { var _ = ApiResponse<string>.Fail(""); });
        }
    }

    [TestClass]
    public class KalkulasiHargaServiceTests
    {
        private AppDbContext _context = null!;
        private KalkulasiHargaService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("KalkulasiTest_" + Guid.NewGuid())
                .Options;
            _context = new AppDbContext(options);

            var repo = new Repository<Menu>(_context);
            repo.Add(new Menu { Nama = "Kopi Susu", Harga = 20000 });
            repo.Add(new Menu { Nama = "Es Teh Manis", Harga = 8000 });

            _service = new KalkulasiHargaService(repo);
        }

        [TestMethod]
        public void HitungTotal_TanpaVariasi_TotalSamaDenganHargaDasar()
        {
            var hasil = _service.HitungTotal(1, new List<MenuVariasi>());
            Assert.AreEqual(20000, hasil.HargaDasar);
            Assert.AreEqual(0, hasil.TotalTambahan);
            Assert.AreEqual(20000, hasil.TotalAkhir);
        }

        [TestMethod]
        public void HitungTotal_DenganExtraShot_TotalBertambah5000()
        {
            var variasi = new List<MenuVariasi>
            {
                new MenuVariasi { NamaVariasi = "Extra Shot", Jumlah = 1 }
            };
            var hasil = _service.HitungTotal(1, variasi);
            Assert.AreEqual(5000, hasil.TotalTambahan);
            Assert.AreEqual(25000, hasil.TotalAkhir);
        }

        [TestMethod]
        public void HitungTotal_DenganBeberapaVariasi_TotalBenar()
        {
            var variasi = new List<MenuVariasi>
            {
                new MenuVariasi { NamaVariasi = "Extra Shot", Jumlah = 1 },
                new MenuVariasi { NamaVariasi = "Oat Milk",   Jumlah = 2 }
            };
            var hasil = _service.HitungTotal(1, variasi);
            Assert.AreEqual(5000 + (8000 * 2), hasil.TotalTambahan);
            Assert.AreEqual(20000 + 5000 + 16000, hasil.TotalAkhir);
        }

        [TestMethod]
        public void HitungTotal_UkuranSmall_TidakMenambahHarga()
        {
            var variasi = new List<MenuVariasi>
            {
                new MenuVariasi { NamaVariasi = "Small", Jumlah = 1 }
            };
            var hasil = _service.HitungTotal(1, variasi);
            Assert.AreEqual(0, hasil.TotalTambahan);
            Assert.AreEqual(20000, hasil.TotalAkhir);
        }

        [TestMethod]
        public void HitungTotal_NamaVariasiCaseInsensitive_TidakError()
        {
            var variasi = new List<MenuVariasi>
            {
                new MenuVariasi { NamaVariasi = "extra shot", Jumlah = 1 }
            };
            var hasil = _service.HitungTotal(1, variasi);
            Assert.AreEqual(5000, hasil.TotalTambahan);
        }

        [TestMethod]
        public void GetTabelVariasi_TidakDapatMengubahTabelAsli()
        {
            var tabel1 = _service.GetTabelVariasi();
            tabel1["Variasi Baru"] = 99999;
            var tabel2 = _service.GetTabelVariasi();
            Assert.IsFalse(tabel2.ContainsKey("Variasi Baru"));
        }

        [TestMethod]
        public void HitungTotal_NamaMenuBenarDiHasil()
        {
            var hasil = _service.HitungTotal(2, new List<MenuVariasi>());
            Assert.AreEqual("Es Teh Manis", hasil.NamaMenu);
        }

        [TestMethod]
        public void HitungTotal_MenuIdNol_ShouldThrow()
        {
            AssertHelper.Throws<ArgumentException>(() => { _service.HitungTotal(0, new List<MenuVariasi>()); });
        }

        [TestMethod]
        public void HitungTotal_MenuIdNegatif_ShouldThrow()
        {
            AssertHelper.Throws<ArgumentException>(() => { _service.HitungTotal(-5, new List<MenuVariasi>()); });
        }

        [TestMethod]
        public void HitungTotal_VariasiNull_ShouldThrow()
        {
            AssertHelper.Throws<ArgumentNullException>(() => { _service.HitungTotal(1, null!); });
        }

        [TestMethod]
        public void HitungTotal_VariasiTidakDikenal_ShouldThrow()
        {
            var variasi = new List<MenuVariasi>
            {
                new MenuVariasi { NamaVariasi = "Topping Aneh XYZ", Jumlah = 1 }
            };
            AssertHelper.Throws<KeyNotFoundException>(() => { _service.HitungTotal(1, variasi); });
        }

        [TestMethod]
        public void HitungTotal_JumlahVariasiNol_ShouldThrow()
        {
            var variasi = new List<MenuVariasi>
            {
                new MenuVariasi { NamaVariasi = "Extra Shot", Jumlah = 0 }
            };
            AssertHelper.Throws<ArgumentException>(() => { _service.HitungTotal(1, variasi); });
        }

        [TestMethod]
        public void HitungTotal_NamaVariasiKosong_ShouldThrow()
        {
            var variasi = new List<MenuVariasi>
            {
                new MenuVariasi { NamaVariasi = "   ", Jumlah = 1 }
            };
            AssertHelper.Throws<ArgumentException>(() => { _service.HitungTotal(1, variasi); });
        }

        [TestMethod]
        public void HitungTotal_MenuTidakAda_ShouldThrow()
        {
            AssertHelper.Throws<KeyNotFoundException>(() => { _service.HitungTotal(999, new List<MenuVariasi>()); });
        }
    }

    [TestClass]
    public class KalkulasiHargaPerformanceTests
    {
        private KalkulasiHargaService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("PerfTest_" + Guid.NewGuid())
                .Options;
            var ctx = new AppDbContext(options);
            var repo = new Repository<Menu>(ctx);
            repo.Add(new Menu { Nama = "Kopi Susu", Harga = 20000 });
            _service = new KalkulasiHargaService(repo);
        }

        [TestMethod]
        public void Performance_1000Kalkulasi_Dibawah100ms()
        {
            var variasi = new List<MenuVariasi>
            {
                new MenuVariasi { NamaVariasi = "Extra Shot", Jumlah = 1 },
                new MenuVariasi { NamaVariasi = "Oat Milk",   Jumlah = 2 },
                new MenuVariasi { NamaVariasi = "Large",      Jumlah = 1 },
            };

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
                _service.HitungTotal(1, variasi);
            sw.Stop();

            Console.WriteLine($"[Performance] 1.000 kalkulasi: {sw.ElapsedMilliseconds}ms");
            Assert.IsLessThan(100L, sw.ElapsedMilliseconds, $"Terlalu lambat: {sw.ElapsedMilliseconds}ms (batas: 100ms)");
        }

        [TestMethod]
        public void Performance_10000_GetTabelVariasi_Dibawah50ms()
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 10000; i++)
                _ = _service.GetTabelVariasi();
            sw.Stop();

            Console.WriteLine($"[Performance] 10.000 GetTabelVariasi: {sw.ElapsedMilliseconds}ms");
            Assert.IsLessThan(50L, sw.ElapsedMilliseconds, $"Terlalu lambat: {sw.ElapsedMilliseconds}ms (batas: 50ms)");
        }

        [TestMethod]
        public void Performance_ApiResponseWrapper_100000_Dibawah200ms()
        {
            var menu = new Menu { Id = 1, Nama = "Kopi Susu", Harga = 20000 };

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
            {
                if (i % 2 == 0) _ = ApiResponse<Menu>.Ok(menu, "Berhasil");
                else _ = ApiResponse<Menu>.Fail("Error contoh");
            }
            sw.Stop();

            Console.WriteLine($"[Performance] 100.000 ApiResponse: {sw.ElapsedMilliseconds}ms");
            Assert.IsLessThan(200L, sw.ElapsedMilliseconds, $"Terlalu lambat: {sw.ElapsedMilliseconds}ms (batas: 200ms)");
        }
    }
}