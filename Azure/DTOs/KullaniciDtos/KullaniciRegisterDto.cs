using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.DTOs.KullaniciDtos
{
    public class KullaniciRegisterDto
    {
        public string KullaniciAdi { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Parola { get; set; } = null!;
    }
}