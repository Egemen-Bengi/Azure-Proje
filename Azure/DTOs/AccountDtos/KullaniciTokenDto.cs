using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure.Models;

namespace Azure.DTOs.AccountDtos
{
    public class KullaniciTokenDto
    {
        public string Id { get; set; } = null!;

        public string KullaniciAdi { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int RolId { get; set; }

        [JsonIgnore]
        public virtual Roller Rol { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}