using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.FilmlerKullanicilarDto;

namespace Azure.Interfaces
{
    public interface IFilmlerKullanicilarRepository
    {
        Task<List<FilmlerKullanicilarDto>> GetAllFilmlerVeKullanicilarAsync();
        Task<List<FilmlerKullanicilarDto>> GetFilmVeKullaniciByKullaniciIdAsync(string id);
        Task<List<FilmlerKullanicilarDto>> GetFilmVeKullaniciByFilmIdAsync(int id);
        Task<FilmlerKullanicilarDto> CreateFilmVeKullaniciAsync(string kullaniciId, int filmId);
        Task<FilmlerKullanicilarDto> DeleteFilmVeKullaniciByIdAsync(string kullaniciId, int filmId);
    }
}