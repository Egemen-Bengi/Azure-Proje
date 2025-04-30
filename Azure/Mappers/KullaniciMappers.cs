using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.KullaniciDtos;
using Azure.Models;

namespace Azure.Mappers
{
    public static class KullaniciMappers
    {
        public static KullaniciDto ToDto(this Kullanicilar kullanici){
            return new KullaniciDto
            {
                KullaniciAdi = kullanici.KullaniciAdi,
                Email = kullanici.Email,
                RolId = kullanici.RolId,
                Id = kullanici.Id,
                Rol = kullanici.Rol
            };
        }

        public static Kullanicilar ToKullanici(this KullaniciRegisterDto dto, Roller rol)
        {
            return new Kullanicilar
            {
                KullaniciAdi = dto.KullaniciAdi,
                Email = dto.Email,
                RolId = rol.Id,
                Rol = rol
            };
        }

        public static void SetHashedPassword(this Kullanicilar user, string hashedPassword){
            user.ParolaH = hashedPassword;
        }

        public static void SetId(this Kullanicilar user, string Id){
            user.Id = Id;
        }

        public static List<KullaniciDto> ToKullaniciDtoList(this List<Kullanicilar> kullanicilarList){
            return kullanicilarList.Select(x => x.ToDto()).ToList();
        }
    }
}