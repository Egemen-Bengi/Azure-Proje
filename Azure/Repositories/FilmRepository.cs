using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.FilmDtos;
using Azure.Interfaces;
using Azure.Mappers;
using Azure.Models;
using Microsoft.EntityFrameworkCore;

namespace Azure.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly OneridbContext _context;

        public FilmRepository(OneridbContext context)
        {
            _context = context;
        }

        public async Task<List<FilmDto>> GetAllFilmlerAsync()
        {
            var filmler = await _context.Filmlers.ToListAsync();
            return filmler.ToDtoList();
        }

        public async Task<FilmDto> GetFilmByIdAsync(int id)
        {
            var film = await _context.Filmlers.FirstOrDefaultAsync(f => f.Id == id)
                ?? throw new Exception("Film bulunamadı!");

            return film.ToDto();
        }

        public async Task<FilmDto> DeleteFilmByIdAsync(int id)
        {
            var film = await _context.Filmlers.FirstOrDefaultAsync(f => f.Id == id)
                ?? throw new Exception("Silinecek film bulunamadı!");

            _context.Filmlers.Remove(film);
            await _context.SaveChangesAsync();

            return film.ToDto();
        }

        public async Task<FilmDto> CreateFilmAsync(FilmCreateDto dto)
        {
            var film = dto.ToEntity();

            _context.Filmlers.Add(film);
            await _context.SaveChangesAsync();

            return film.ToDto();
        }

        public async Task<FilmDto> UpdateFilmAsync(int id, FilmUpdateDto dto)
        {
            var film = await _context.Filmlers.FirstOrDefaultAsync(f => f.Id == id)
                ?? throw new Exception("Güncellenecek film bulunamadı!");

            film.UpdateEntity(dto);

            await _context.SaveChangesAsync();

            return film.ToDto();
        }
    }
}