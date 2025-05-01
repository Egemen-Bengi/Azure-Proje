using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.RolDtos;
using Azure.Models;

namespace Azure.Mappers
{
    public static class RolMappers
    {
        public static RolDto ToDto(this Roller roller)
        {
            return new RolDto
            {
                Id = roller.Id,
                RolAdi = roller.RolAdi,
                Kullanicilars = roller.Kullanicilars
            };
        }

        public static Roller ToModel(this RolDto rolDto)
        {
            return new Roller
            {
                Id = rolDto.Id,
                RolAdi = rolDto.RolAdi,
                Kullanicilars = rolDto.Kullanicilars
            };
        }

        public static Roller ToModel(this RolCreateDto rolCreateDto)
        {
            return new Roller
            {
                RolAdi = rolCreateDto.RolAdi
            };
        }

        public static Roller ToModel(this RolUpdateDto rolUpdateDto)
        {
            return new Roller
            {
                Id = rolUpdateDto.Id,
                RolAdi = rolUpdateDto.RolAdi
            };
        }

        public static List<RolDto> ToDtoList(this List<Roller> rollerList)
        {
            return rollerList.Select(roller => roller.ToDto()).ToList();
        }


    }
}