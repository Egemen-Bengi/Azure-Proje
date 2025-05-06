using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.FilmDtos;

namespace Azure.Interfaces
{
    public interface IFilmRepository
    {
        Task<List<FilmDto>> GetAllFilmlerAsync();
        Task<FilmDto> GetFilmByIdAsync(int id);
        Task<FilmDto> DeleteFilmByIdAsync(int id);
        Task<FilmDto> CreateFilmAsync(FilmCreateDto dto);
        Task<FilmDto> UpdateFilmAsync(int id, FilmUpdateDto dto);
    }
}