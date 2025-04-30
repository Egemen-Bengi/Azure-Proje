using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Azure.DTOs.KullaniciDtos;
using Azure.Interfaces;
using Azure.Mappers;
using Azure.Models;
using Microsoft.EntityFrameworkCore;

namespace Azure.Repositories
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly OneridbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        public KullaniciRepository(OneridbContext context, IPasswordService passwordService, ITokenService tokenService)
        {
            _context = context;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }
        public async Task<KullaniciDto> LoginKullaniciAsync(KullaniciLoginDto loginDto)
        {
            var kullanici = await _context.Kullanicilars.FirstOrDefaultAsync(x => x.KullaniciAdi == loginDto.KullaniciAdi) ?? 
            throw new Exception("Boyle bir kullanici yok!");
        
            if(_passwordService.VerifyPassword(kullanici, loginDto.Parola, kullanici.ParolaH) == false) throw new Exception("Kullanici adi veya parola hatali!");

            var kullaniciDto = kullanici.ToDto(_tokenService.CreateToken(kullanici));
            return kullaniciDto;
        }

        public async Task<KullaniciDto> RegisterKullaniciAsync(KullaniciRegisterDto registerDto)
        {
            if (await _context.Kullanicilars.AnyAsync(x => x.KullaniciAdi == registerDto.KullaniciAdi)) throw new Exception("Bu kullanici adi zaten alindi!");
            if (await _context.Kullanicilars.AnyAsync(x => x.Email == registerDto.Email)) throw new Exception("Bu email zaten alindi!");

            var kullaniciRol = await _context.Rollers.FirstOrDefaultAsync(x => x.RolAdi == "Standart") ?? throw new Exception("Kullaniciya rol verilirken bir sorun oluştu!");
            var kullanici = registerDto.ToKullanici(kullaniciRol);
            var hashedPassword = _passwordService.HashPassword(kullanici, registerDto.Parola);
            
            kullanici.SetHashedPassword(hashedPassword);

            var addedKullanici = await _context.AddAsync(kullanici) ?? throw new Exception("Kullanici Olusturulamadi!");
            await _context.SaveChangesAsync();
            var addedKullaniciDto = addedKullanici.Entity.ToDto(_tokenService.CreateToken(addedKullanici.Entity));

            return addedKullaniciDto;
            
        }
    }
}