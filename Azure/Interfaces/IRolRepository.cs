using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.RolDtos;
using Azure.Models;

namespace Azure.Interfaces
{
    public interface IRolRepository
    {
        public Task<Roller> CreateRolAsync(RolCreateDto rolCreateDto);
        public Task<RolDto> UpdateRolAsync(RolUpdateDto rolUpdateDto);
        public Task<Roller> DeleteRolByIdAsync(int rolId);
        public Task<RolDto> GetRolByIdAsync(int rolId);
        public Task<List<RolDto>> GetRollerAsync();
    }
}