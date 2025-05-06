using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Azure.Models;

namespace Azure.DTOs.FilmlerKullanicilarDto
{
    public class FilmlerKullanicilarDto
    {
        public string? KullaniciId { get; set; }

        public int? FilmId { get; set; }

        public virtual Filmler? Film { get; set; }
        [JsonIgnore]
        public virtual Kullanicilar? Kullanici { get; set; }
    }
}