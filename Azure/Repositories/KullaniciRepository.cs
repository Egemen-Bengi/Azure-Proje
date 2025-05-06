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

        public async Task<List<KullaniciDto>> GetAllKullanicilar()
        {
            var kullaniciList = await _context.Kullanicilars.Include(x => x.Rol).ToListAsync();
            return kullaniciList.ToKullaniciDtoList();
        }

        public async Task<KullaniciDto> GetKullaniciByEmail(string email)
        {
            var kullanici = await _context.Kullanicilars.Include(x => x.Rol).FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception("Kullanici Bulunamadi");
            return kullanici.ToDto();
        }

        public async Task<KullaniciDto> GetKullaniciById(string id)
        {
            var kullanici = await _context.Kullanicilars.Include(x => x.Rol).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Kullanici Bulunamadi");
            return kullanici.ToDto();
        }

        public async Task<Kullanicilar> CreateKullaniciAsync(KullaniciRegisterDto registerDto)
        {
            var kullaniciRol = await _context.Rollers.FirstOrDefaultAsync(x => x.RolAdi == "Standart") ?? throw new Exception("Kullaniciya rol verilirken bir sorun oluştu!");
            var kullanici = registerDto.ToKullanici(kullaniciRol);
            var hashedPassword = _passwordService.HashPassword(kullanici, registerDto.Parola);
            var kullaniciId = Guid.NewGuid().ToString();

            if(await _context.Kullanicilars.FirstOrDefaultAsync(x => x.Id == kullaniciId) != null) throw new Exception("GUID imkansizi basardi");

            kullanici.SetId(kullaniciId);
            kullanici.SetHashedPassword(hashedPassword);

            var addedKullanici = await _context.AddAsync(kullanici) ?? throw new Exception("Kullanici Olusturulamadi!");
            await _context.SaveChangesAsync();

            return addedKullanici.Entity;
        }

        public async Task<Kullanicilar> DeleteKullaniciById(string id)
        {
            var kullanici = await _context.Kullanicilars.Include(x => x.Rol).FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Kullanici bulunamadi");

            _context.Kullanicilars.Remove(kullanici);
            await _context.SaveChangesAsync();

            return kullanici;
        }
    }
}