using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Interfaces;
using Azure.Models;
using Microsoft.AspNetCore.Identity;

namespace Azure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<Kullanicilar> _passwordHasher;
        public PasswordService(IPasswordHasher<Kullanicilar> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public string HashPassword(Kullanicilar user, string password)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, password);
            return hashedPassword;
        }

        public bool VerifyPassword(Kullanicilar user, string password, string hashedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}