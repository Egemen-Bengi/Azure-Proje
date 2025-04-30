using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.DTOs.KullaniciDtos
{
    public class KullaniciLoginDto
    {
        public string KullaniciAdi { get; set; } = null!;

        public string Parola { get; set; } = null!;
    }
}