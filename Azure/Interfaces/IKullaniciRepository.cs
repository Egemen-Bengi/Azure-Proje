using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.KullaniciDtos;
using Azure.Models;

namespace Azure.Interfaces
{
    public interface IKullaniciRepository
    {
        public Task<Kullanicilar> CreateKullaniciAsync(KullaniciRegisterDto registerDto);
        public Task<List<KullaniciDto>> GetAllKullanicilar();
        public Task<KullaniciDto> GetKullaniciById(string id);
        public Task<KullaniciDto> GetKullaniciByEmail(string email);
        public Task<Kullanicilar> DeleteKullaniciById(string id);
    }
}