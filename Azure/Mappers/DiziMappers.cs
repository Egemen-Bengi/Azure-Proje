using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.DiziDtos;
using Azure.Models;

namespace Azure.Mappers
{
    public static class DiziMappers
    {
        public static DiziDto ToDto(this Diziler dizi)
        {
            return new DiziDto
            {
                Id = dizi.Id,
                DiziAdi = dizi.DiziAdi,
                DiziAciklamasi = dizi.DiziAciklamasi,
                SezonSayisi = dizi.SezonSayisi,
                Sure = dizi.Sure,
                Tarih = dizi.Tarih
            };
        }

        public static List<DiziDto> ToDtoList(this List<Diziler> list)
        {
            return list.Select(d => d.ToDto()).ToList(); 
        }
    }
}