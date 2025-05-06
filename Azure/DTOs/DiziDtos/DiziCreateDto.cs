using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.DTOs.DiziDtos
{
    public class DiziCreateDto
    {
        public string DiziAdi { get; set; } = null!;

        public string DiziAciklamasi { get; set; } = null!;

        public DateOnly Tarih { get; set; }

        public int Sure { get; set; }

        public int SezonSayisi { get; set; }
    }
}