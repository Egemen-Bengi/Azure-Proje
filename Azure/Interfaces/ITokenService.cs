using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Models;

namespace Azure.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(Kullanicilar kullanici);
        public bool ValidateToken(string token);
    }
}