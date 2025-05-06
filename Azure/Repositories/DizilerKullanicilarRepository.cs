using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.DizilerKullanicilarDtos;
using Azure.Interfaces;
using Azure.Mappers;
using Azure.Models;
using Microsoft.EntityFrameworkCore;

namespace Azure.Repositories
{
    public class DizilerKullanicilarRepository : IDizilerKullanicilarRepository
    {
        private readonly OneridbContext _context;
        public DizilerKullanicilarRepository(OneridbContext context)
        {
            _context = context;
        }
        public async Task<DizilerKullanicilarDto> CreateDiziVeKullaniciAsync(string KullaniciId, int diziId)
        {
            // Kullanıcıyı kontrol ediyoruz
            var kullanici = await _context.Kullanicilars
                .FirstOrDefaultAsync(k => k.Id == KullaniciId)
                ?? throw new Exception("Kullanıcı bulunamadı.");

            if (kullanici.Dizis.Any(d => d.Id == diziId))
            {
                throw new Exception("Bu kullanıcı zaten bu diziyle ilişkilendirilmiş.");
            }

            // Diziyi kontrol ediyoruz
            var dizi = await _context.Dizilers
                .FirstOrDefaultAsync(d => d.Id == diziId)
                ?? throw new Exception("Dizi bulunamadı.");

            // Kullanıcı ile dizi arasındaki ilişkiyi ekliyoruz
            kullanici.Dizis.Add(dizi);

            // Değişiklikleri veritabanına kaydediyoruz
            await _context.SaveChangesAsync();

            // Eklenen ilişkiyi DTO olarak döndürüyoruz
            return new DizilerKullanicilarDto
            {
                KullaniciId = kullanici.Id,
                DiziId = dizi.Id,
                Kullanici = kullanici,
                Dizi = dizi
            };
        }

        public async Task<DizilerKullanicilarDto> DeleteDiziVeKullaniciByIdAsync(string kullaniciId, int diziId)
        {
            // Kullanıcıyı ve ilişkili diziyi ara tablodan buluyoruz
            var kullanici = await _context.Kullanicilars
                .Include(k => k.Dizis)
                .FirstOrDefaultAsync(k => k.Id == kullaniciId) 
                ?? throw new Exception("Kullanıcı bulunamadı.");

            if (!kullanici.Dizis.Any(d => d.Id == diziId))
            {
                throw new Exception("Bu kullanıcı ile bu dizi arasında bir ilişki bulunamadı.");
            }

            // İlgili diziyi kullanıcının dizilerinden buluyoruz
            var dizi = kullanici.Dizis.FirstOrDefault(d => d.Id == diziId)
                ?? throw new Exception("Dizi bulunamadı.");

            // Kullanıcı ile dizi arasındaki ilişkiyi kaldırıyoruz
            kullanici.Dizis.Remove(dizi);

            // Değişiklikleri veritabanına kaydediyoruz
            await _context.SaveChangesAsync();

            // Silinen ilişkiyi DTO olarak döndürüyoruz
            return new DizilerKullanicilarDto
            {
                KullaniciId = kullanici.Id,
                DiziId = dizi.Id,
                Kullanici = kullanici,
                Dizi = dizi
            };
        }

        public async Task<List<DizilerKullanicilarDto>> GetAllDizilerVeKullanicilarAsync()
        {
            // Kullanıcıları ve ilişkili dizileri veritabanından çekiyoruz
            var dizilerFromKullanicilar = await _context.Kullanicilars.Include(x => x.Dizis).ToListAsync();

            // Kullanıcı-Dizi eşleşmelerini bir listeye dönüştürüyoruz
            var list = dizilerFromKullanicilar
                .SelectMany(kullanici => kullanici.Dizis.Select(dizi => new DizilerKullanicilarDto
                {
                    KullaniciId = kullanici.Id,
                    DiziId = dizi.Id,
                    Kullanici = kullanici,
                    Dizi = dizi
                }))
                .ToList();

            return list;
        }

        public async Task<List<DizilerKullanicilarDto>> GetDiziVeKullaniciByDiziIdAsync(int id)
        {
            var dizilerFromKullanicilar = await _context.Dizilers.Include(x => x.Kullanicis).Where(d => d.Id == id).ToListAsync();

            var list = dizilerFromKullanicilar
                .SelectMany(dizi => dizi.Kullanicis.Select(kullanici => new DizilerKullanicilarDto
                {
                    KullaniciId = kullanici.Id,
                    DiziId = dizi.Id,
                    Kullanici = kullanici,
                    Dizi = dizi
                }))
                .ToList();
            
            return list;
        }

        public async Task<List<DizilerKullanicilarDto>> GetDiziVeKullaniciByKullaniciIdAsync(string id)
        {
            
            var dizilerFromKullanicilar = await _context.Kullanicilars.Include(x => x.Dizis).Where(k => k.Id == id).ToListAsync();

            var list = dizilerFromKullanicilar
                .SelectMany(kullanici => kullanici.Dizis.Select(dizi => new DizilerKullanicilarDto
                {
                    KullaniciId = kullanici.Id,
                    DiziId = dizi.Id,
                    Kullanici = kullanici,
                    Dizi = dizi
                }))
                .ToList();

            return list;
        }
    }
}