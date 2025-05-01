using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.DTOs.RolDtos
{
    public class RolUpdateDto
    {
        public int Id { get; set; }

        public string RolAdi { get; set; } = null!;
    }
}