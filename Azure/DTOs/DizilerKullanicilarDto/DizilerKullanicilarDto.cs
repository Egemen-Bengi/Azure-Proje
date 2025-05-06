using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure.Models;

namespace Azure.DTOs.DizilerKullanicilarDtos
{
    public class DizilerKullanicilarDto
    {
        public string? KullaniciId { get; set; }

        public int? DiziId { get; set; }

        public virtual Diziler? Dizi { get; set; }
        [JsonIgnore]
        public virtual Kullanicilar? Kullanici { get; set; }
    }
}