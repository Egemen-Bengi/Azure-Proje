using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.DiziDtos;

namespace Azure.Interfaces
{
    public interface IDiziRepository
    {
        public Task<List<DiziDto>> GetAllDizilerAsync();
        public Task<DiziDto> GetDiziByIdAsync(int id);
        public Task<DiziDto> DeleteDiziByIdAsync(int id);
        public Task<DiziDto> CreateDiziAsync(DiziCreateDto dto);
        public Task<DiziDto> UpdateDiziAsync(int id, DiziUpdateDto dto);
    }
}