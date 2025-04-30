using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.AccountDtos;
using Azure.DTOs.KullaniciDtos;
using Azure.Interfaces;
using Azure.Mappers;
using Azure.Models;
using Microsoft.EntityFrameworkCore;

namespace Azure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly OneridbContext _context;
        private readonly IKullaniciRepository _kullaniciRepo;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        public AccountRepository(IKullaniciRepository kullaniciRepo, OneridbContext context, 
                                 IPasswordService passwordService,
                                 ITokenService tokenService)
        {
            _kullaniciRepo = kullaniciRepo;
            _context = context;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }
        
        public async Task<KullaniciTokenDto> LoginKullaniciAsync(KullaniciLoginDto loginDto)
        {
            var kullanici = await _context.Kullanicilars.FirstOrDefaultAsync(x => x.KullaniciAdi == loginDto.KullaniciAdi) ?? 
            throw new Exception("Boyle bir kullanici yok!");
        
            if(_passwordService.VerifyPassword(kullanici, loginDto.Parola, kullanici.ParolaH) == false) throw new Exception("Kullanici adi veya parola hatali!");

            var kullaniciTokenDto = kullanici.ToTokenDto(_tokenService.CreateToken(kullanici));
            return kullaniciTokenDto;
        }

        public async Task<KullaniciTokenDto> RegisterKullaniciAsync(KullaniciRegisterDto registerDto)
        {
            if (await _context.Kullanicilars.AnyAsync(x => x.KullaniciAdi == registerDto.KullaniciAdi)) throw new Exception("Bu kullanici adi zaten alindi!");
            if (await _context.Kullanicilars.AnyAsync(x => x.Email == registerDto.Email)) throw new Exception("Bu email zaten alindi!");

            var kullanici = await _kullaniciRepo.CreateKullaniciAsync(registerDto);

            var kullaniciTokenDto = kullanici.ToTokenDto(_tokenService.CreateToken(kullanici));

            return kullaniciTokenDto;
        }
    }
}