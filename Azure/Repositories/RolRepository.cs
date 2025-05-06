using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.RolDtos;
using Azure.Interfaces;
using Azure.Mappers;
using Azure.Models;
using Microsoft.EntityFrameworkCore;

namespace Azure.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly OneridbContext _context;
        public RolRepository(OneridbContext context)
        {
            _context = context;
        }

        public async Task<Roller> CreateRolAsync(RolCreateDto rolCreateDto)
        {
            var existedRol = await _context.Rollers.FirstOrDefaultAsync(x => x.RolAdi == rolCreateDto.RolAdi);
            if(existedRol != null) throw new Exception("Boyle bir rol zaten var!");

            var rol = rolCreateDto.ToModel();

            try
            {
                var addedRol = await _context.Rollers.AddAsync(rol);
                await _context.SaveChangesAsync();
            
                return addedRol.Entity;
            }
            catch(Exception)
            {
                throw new Exception("Rol eklenirken bir sorun olustu");
            }
        }

        public async Task<Roller> DeleteRolByIdAsync(int rolId)
        {
            var rol = await _context.Rollers.FirstOrDefaultAsync(x => x.Id == rolId) ?? throw new Exception("Silinecek Rol bulunamadi!");

            try
            {
                _context.Rollers.Remove(rol);
                await _context.SaveChangesAsync();
                return rol;
            }
            catch(Exception)
            {
                throw new Exception("Rol silinirken bir seyler ters gitti: ");
            }
        }

        public async Task<RolDto> GetRolByIdAsync(int rolId)
        {
            var rol = await _context.Rollers.Include(x => x.Kullanicilars).FirstOrDefaultAsync(x => x.Id == rolId) ?? throw new Exception("Rol Bulunamadi!");

            return rol.ToDto();
        }

        public async Task<List<RolDto>> GetRollerAsync()
        {
            var roller = await _context.Rollers.Include(x => x.Kullanicilars).ToListAsync();
            return roller.ToDtoList();
        }

        public async Task<RolDto> UpdateRolAsync(RolUpdateDto rolUpdateDto)
        {
            var rol = await _context.Rollers.FirstOrDefaultAsync(x => x.Id == rolUpdateDto.Id) ?? throw new Exception("Guncellenecek Rol bulunamadi!");

            rol.RolAdi = rolUpdateDto.RolAdi;

            try
            {
                await _context.SaveChangesAsync();
                return rol.ToDto();
            }
            catch(Exception)
            {
                throw new Exception("Rol guncellenirken bir sorun olustu");
            }
        }
    }
}