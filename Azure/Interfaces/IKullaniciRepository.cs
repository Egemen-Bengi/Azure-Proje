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
        public Task<KullaniciDto> RegisterKullaniciAsync(KullaniciRegisterDto registerDto);
        public Task<KullaniciDto> LoginKullaniciAsync(KullaniciLoginDto loginDto);
    }
}