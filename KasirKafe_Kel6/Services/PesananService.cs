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

                    if (menu.Contains("kopi susu") || menu.Contains("latte"))
                    {
                        _bahanBakuService.UpdateStokBahan(1, pesanan.Jumlah * -1);
                        _bahanBakuService.UpdateStokBahan(2, pesanan.Jumlah * -1);
                    }
                    else if (menu.Contains("kopi hitam") || menu.Contains("americano"))
                    {
                        _bahanBakuService.UpdateStokBahan(1, pesanan.Jumlah * -1);
                    }

                    pesanan.ProsesPesanan();
                }
                catch (InvalidOperationException ex)
                {
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