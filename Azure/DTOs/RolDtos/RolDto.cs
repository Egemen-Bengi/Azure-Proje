using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure.Models;

namespace Azure.DTOs.RolDtos
{
    public class RolDto
    {
        public int Id { get; set; }

        public string RolAdi { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Kullanicilar> Kullanicilars { get; set; } = new List<Kullanicilar>();
    }
}