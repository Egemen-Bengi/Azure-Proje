using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.DiziDtos;
using Azure.Interfaces;
using Azure.Mappers;
using Azure.Models;
using Microsoft.EntityFrameworkCore;

namespace Azure.Repositories
{
    public class DiziRepository : IDiziRepository
    {
        private readonly OneridbContext _context;
        public DiziRepository(OneridbContext context)
        {
            _context = context;
        }
        public Task<DiziDto> CreateDiziAsync(DiziCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DiziDto> DeleteDiziByIdAsync(int id)
        {
            var dizi = await _context.Dizilers.FirstOrDefaultAsync(d => d.Id == id) ?? throw new Exception("Silinecek dizi bulunamadi!");

            _context.Remove(dizi);
            await _context.SaveChangesAsync();

            return dizi.ToDto();
        }

        public async Task<List<DiziDto>> GetAllDizilerAsync()
        {
            var diziler = await _context.Dizilers.ToListAsync();

            return diziler.ToDtoList();
        }

        public async Task<DiziDto> GetDiziByIdAsync(int id)
        {
            var dizi = await _context.Dizilers.FirstOrDefaultAsync(d => d.Id == id) ?? throw new Exception("Silinecek dizi bulunamadi!");

            return dizi.ToDto();
        }

        public async Task<DiziDto> UpdateDiziAsync(int id, DiziUpdateDto dto)
        {
            var dizi = await _context.Dizilers.FirstOrDefaultAsync(d => d.Id == id) ?? throw new Exception("Silinecek dizi bulunamadi!");

            dizi.DiziAciklamasi = dto.DiziAciklamasi;
            dizi.DiziAdi = dto.DiziAdi;
            dizi.SezonSayisi = dto.SezonSayisi;
            dizi.Sure = dto.Sure;
            dizi.Tarih = dto.Tarih;

            await _context.SaveChangesAsync();

            return dizi.ToDto();
        }
    }
}