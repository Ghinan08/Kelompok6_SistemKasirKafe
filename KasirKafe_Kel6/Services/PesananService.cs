using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using KasirKafe_Kel6.DTO;
using System;
using System.Collections.Generic;

namespace KasirKafe_Kel6.Services
{
    public class PesananService
    {
        private readonly IRepository<Pesanan> _pesananRepo;
        private readonly BahanBakuService _bahanBakuService;

        public PesananService(IRepository<Pesanan> pesananRepo, BahanBakuService bahanBakuService)
        {
            _pesananRepo = pesananRepo;
            _bahanBakuService = bahanBakuService;
        }

        public Pesanan BuatPesanan(PesananRequest request)
        {
            var pesanan = new Pesanan(request.NamaMenu, request.Jumlah, request.TotalHarga);
            _pesananRepo.Add(pesanan);

            return pesanan;
        }

        public IEnumerable<Pesanan> GetAllPesanan()
        {
            return _pesananRepo.GetAll();
        }

        public Pesanan? UpdateStatusPesanan(int id)
        {
            var pesanan = _pesananRepo.GetById(id);
            if (pesanan == null) return null;

            if (pesanan.Status == StatusPesanan.Dipesan)
            {
                try
                {
                    string menu = pesanan.NamaMenu.ToLower();

                    // Skenario 1: Kopi Susu butuh Biji Kopi (ID:1) dan Susu (ID:2)
                    if (menu.Contains("kopi susu") || menu.Contains("latte"))
                    {
                        _bahanBakuService.UpdateStokBahan(1, pesanan.Jumlah * -1);
                        _bahanBakuService.UpdateStokBahan(2, pesanan.Jumlah * -1);
                    }
                    // Skenario 2: Kopi Hitam cuma butuh Biji Kopi (ID:1)
                    else if (menu.Contains("kopi hitam") || menu.Contains("americano"))
                    {
                        _bahanBakuService.UpdateStokBahan(1, pesanan.Jumlah * -1);
                    }

                    // Jika stok aman dan berhasil dikurangi, ubah status pesanan
                    pesanan.ProsesPesanan();
                }
                catch (InvalidOperationException ex)
                {
                    // Jika stok di gudang kurang, gagalkan prosesnya!
                    throw new InvalidOperationException($"Gagal diproses! {ex.Message}");
                }
            }
            else if (pesanan.Status == StatusPesanan.Diproses)
            {
                pesanan.SelesaikanPesanan();
            }
            else
            {
                throw new InvalidOperationException("Pesanan sudah selesai atau dibatalkan.");
            }

            _pesananRepo.Update(pesanan);
            return pesanan;
        }
    }
}