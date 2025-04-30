using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Models;

namespace Azure.Interfaces
{
    public interface IPasswordService
    {
        public string HashPassword(Kullanicilar user, string password);
        public bool VerifyPassword(Kullanicilar user, string password, string hashedPassword);
    }
}