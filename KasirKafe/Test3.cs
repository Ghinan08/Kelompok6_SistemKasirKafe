using KasirKafe;
using KasirKafe_Kel6.Data;
using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using KasirKafe_Kel6.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Diagnostics;

// Percobaan automata dan kode reuse -Erland

[TestClass]
public class Test3
{
    [TestMethod]
    public void Automata_BahanBaku_StateHabis_Berhasil()
    {
        var bahan = new BahanBaku("Biji Kopi", 10);
        bahan.UpdateStok(-10);

        Assert.AreEqual(0, bahan.Stok);
        Assert.AreEqual("Habis", bahan.Status);
    }

    [TestMethod]
    public void Automata_BahanBaku_StateDiRestockLaluBanyak_Berhasil()
    {
        var bahan = new BahanBaku("Susu", 0);
        Assert.AreEqual("Habis", bahan.Status);

        bahan.UpdateStok(50);

        bahan.UpdateStatusAutomata();
        Assert.AreEqual("Tersedia Banyak", bahan.Status);
    }

    [TestMethod]
    public void DbC_BahanBaku_StokNegatif_ShouldThrowException()
    {
        var bahan = new BahanBaku("Sirup", 5);
        AssertHelper.Throws<InvalidOperationException>(() => bahan.UpdateStok(-10));
    }

    [TestMethod]
    public void PencetakanService_GenerateUUID_Berhasil()
    {
        var service = new PencetakanService();
        string resi1 = service.GenerateNomorResi();
        string resi2 = service.GenerateNomorResi();

        Assert.IsNotNull(resi1);
        Assert.AreNotEqual(resi1, resi2);
    }

    [TestMethod]
    public void Performance_1000CetakStruk_Dibawah100ms()
    {
        var service = new PencetakanService();
        var pesananDummy = new Pesanan { NamaMenu = "Espresso", Jumlah = 1, TotalHarga = 20000 };

        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            service.GenerateStrukPDFText(pesananDummy);
        }
        sw.Stop();

        Console.WriteLine($"[Performance Test] 1.000 cetak struk: {sw.ElapsedMilliseconds}ms");
        Assert.IsLessThan(100L, sw.ElapsedMilliseconds, $"Terlalu lambat: {sw.ElapsedMilliseconds}ms (batas 100ms)");
    }
}
