using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.FilmlerKullanicilarDto;
using Azure.Interfaces;
using Azure.Models;
using Microsoft.EntityFrameworkCore;

namespace Azure.Repositories
{
    public class FilmlerKullanicilarRepository : IFilmlerKullanicilarRepository
    {
        private readonly OneridbContext _context;

        public FilmlerKullanicilarRepository(OneridbContext context)
        {
            _context = context;
        }

        public async Task<FilmlerKullanicilarDto> CreateFilmVeKullaniciAsync(string kullaniciId, int filmId)
        {
            var kullanici = await _context.Kullanicilars
                .FirstOrDefaultAsync(k => k.Id == kullaniciId)
                ?? throw new Exception("Kullanıcı bulunamadı.");

            if (kullanici.Films.Any(f => f.Id == filmId))
            {
                throw new Exception("Bu kullanıcı zaten bu filmle ilişkilendirilmiş.");
            }

            var film = await _context.Filmlers
                .FirstOrDefaultAsync(f => f.Id == filmId)
                ?? throw new Exception("Film bulunamadı.");

            kullanici.Films.Add(film);
            await _context.SaveChangesAsync();

            return new FilmlerKullanicilarDto
            {
                KullaniciId = kullanici.Id,
                FilmId = film.Id,
                Kullanici = kullanici,
                Film = film
            };
        }

        public async Task<FilmlerKullanicilarDto> DeleteFilmVeKullaniciByIdAsync(string kullaniciId, int filmId)
        {
            var kullanici = await _context.Kullanicilars
                .Include(k => k.Films)
                .FirstOrDefaultAsync(k => k.Id == kullaniciId)
                ?? throw new Exception("Kullanıcı bulunamadı.");

            if (!kullanici.Films.Any(f => f.Id == filmId))
            {
                throw new Exception("Bu kullanıcı ile bu film arasında bir ilişki bulunamadı.");
            }

            var film = kullanici.Films.FirstOrDefault(f => f.Id == filmId)
                ?? throw new Exception("Film bulunamadı.");

            kullanici.Films.Remove(film);
            await _context.SaveChangesAsync();

            return new FilmlerKullanicilarDto
            {
                KullaniciId = kullanici.Id,
                FilmId = film.Id,
                Kullanici = kullanici,
                Film = film
            };
        }

        public async Task<List<FilmlerKullanicilarDto>> GetAllFilmlerVeKullanicilarAsync()
        {
            var filmlerFromKullanicilar = await _context.Kullanicilars.Include(x => x.Films).ToListAsync();

            var list = filmlerFromKullanicilar
                .SelectMany(kullanici => kullanici.Films.Select(film => new FilmlerKullanicilarDto
                {
                    KullaniciId = kullanici.Id,
                    FilmId = film.Id,
                    Kullanici = kullanici,
                    Film = film
                }))
                .ToList();

            return list;
        }

        public async Task<List<FilmlerKullanicilarDto>> GetFilmVeKullaniciByFilmIdAsync(int id)
        {
            var filmlerFromKullanicilar = await _context.Filmlers.Include(x => x.Kullanicis).Where(f => f.Id == id).ToListAsync();

            var list = filmlerFromKullanicilar
                .SelectMany(film => film.Kullanicis.Select(kullanici => new FilmlerKullanicilarDto
                {
                    KullaniciId = kullanici.Id,
                    FilmId = film.Id,
                    Kullanici = kullanici,
                    Film = film
                }))
                .ToList();

            return list;
        }

        public async Task<List<FilmlerKullanicilarDto>> GetFilmVeKullaniciByKullaniciIdAsync(string id)
        {
            var filmlerFromKullanicilar = await _context.Kullanicilars.Include(x => x.Films).Where(k => k.Id == id).ToListAsync();

            var list = filmlerFromKullanicilar
                .SelectMany(kullanici => kullanici.Films.Select(film => new FilmlerKullanicilarDto
                {
                    KullaniciId = kullanici.Id,
                    FilmId = film.Id,
                    Kullanici = kullanici,
                    Film = film
                }))
                .ToList();

            return list;
        }
    }
}