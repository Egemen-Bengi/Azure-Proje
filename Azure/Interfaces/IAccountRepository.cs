using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.AccountDtos;
using Azure.DTOs.KullaniciDtos;

namespace Azure.Interfaces
{
    public interface IAccountRepository
    {
        public Task<KullaniciTokenDto> RegisterKullaniciAsync(KullaniciRegisterDto registerDto);
        public Task<KullaniciTokenDto> LoginKullaniciAsync(KullaniciLoginDto loginDto);

    }
}