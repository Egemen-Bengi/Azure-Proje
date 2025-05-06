using System;
using System.Collections.Generic;
using System.Linq;
using Azure.DTOs.FilmDtos;
using Azure.Models;

namespace Azure.Mappers
{
    public static class FilmMappers
    {
        public static FilmDto ToDto(this Filmler film)
        {
            return new FilmDto
            {
                Id = film.Id,
                FilmAdi = film.FilmAdi,
                FilmAciklamasi = film.FilmAciklamasi,
                Tarih = film.Tarih,
                Sure = film.Sure
            };
        }

        public static List<FilmDto> ToDtoList(this List<Filmler> list)
        {
            return list.Select(f => f.ToDto()).ToList();
        }

        public static Filmler ToEntity(this FilmCreateDto dto)
        {
            return new Filmler
            {
                FilmAdi = dto.FilmAdi,
                FilmAciklamasi = dto.FilmAciklamasi,
                Tarih = dto.Tarih,
                Sure = dto.Sure
            };
        }

        public static void UpdateEntity(this Filmler film, FilmUpdateDto dto)
        {
            film.FilmAdi = dto.FilmAdi;
            film.FilmAciklamasi = dto.FilmAciklamasi;
            film.Tarih = dto.Tarih;
            film.Sure = dto.Sure;
        }
    }
}