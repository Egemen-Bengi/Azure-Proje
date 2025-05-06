using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.DTOs.FilmDtos
{
    public class FilmCreateDto
    {
        public string FilmAdi { get; set; } = null!;

        public string FilmAciklamasi { get; set; } = null!;

        public DateOnly Tarih { get; set; }

        public int Sure { get; set; }
    }
}