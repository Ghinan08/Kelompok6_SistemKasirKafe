using KasirKafe_Kel6.Models;
using KasirKafe_Kel6.Repositories;
using System.Collections.Generic;

namespace KasirKafe_Kel6.Services
{
    public class BahanBakuService
    {
        private readonly IRepository<BahanBaku> _repo;

        public BahanBakuService(IRepository<BahanBaku> repo)
        {
            _repo = repo;
        }

        public BahanBaku TambahBahanBaru(string nama, int stok)
        {
            var bahan = new BahanBaku(nama, stok);
            _repo.Add(bahan);
            return bahan;
        }

        public BahanBaku UpdateStokBahan(int id, int perubahanStok)
        {
            var bahan = _repo.GetById(id);
            if (bahan != null)
            {
                bahan.UpdateStok(perubahanStok);
                _repo.Update(bahan);
            }
            return bahan;
        }

        public IEnumerable<BahanBaku> GetAllBahanBaku()
        {
            return _repo.GetAll();
        }

        public BahanBaku GetBahanBakuById(int id)
        {
            return _repo.GetById(id);
        }

        public bool DeleteBahanBaku(int id)
        {
            var bahan = _repo.GetById(id);
            if (bahan == null) return false;

            _repo.Delete(id);
            return true;
        }
    }
}