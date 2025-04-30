using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.AccountDtos;
using Azure.Models;

namespace Azure.Mappers
{
    public static class AccountMappers
    {
        public static KullaniciTokenDto ToTokenDto(this Kullanicilar user, string token)
        {
            return new KullaniciTokenDto
            {
                Id = user.Id,
                Email = user.Email,
                KullaniciAdi = user.KullaniciAdi,
                RolId = user.RolId,
                Rol = user.Rol,
                Token = token
            };
        }
    }
}