using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.DizilerKullanicilarDtos;

namespace Azure.Interfaces
{
    public interface IDizilerKullanicilarRepository
    {
        public Task<List<DizilerKullanicilarDto>> GetAllDizilerVeKullanicilarAsync();
        public Task<List<DizilerKullanicilarDto>> GetDiziVeKullaniciByKullaniciIdAsync(string id);
        public Task<List<DizilerKullanicilarDto>> GetDiziVeKullaniciByDiziIdAsync(int id);
        public Task<DizilerKullanicilarDto> CreateDiziVeKullaniciAsync(string KullaniciId, int diziId);
        public Task<DizilerKullanicilarDto> DeleteDiziVeKullaniciByIdAsync(string kullaniciId, int diziId);
    }
}