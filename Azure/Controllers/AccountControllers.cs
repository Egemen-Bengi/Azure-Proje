using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.DTOs.KullaniciDtos;
using Azure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Azure.Controllers
{
    public class AccountController
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ITokenService _tokenService;
        public AccountController(IAccountRepository accountRepository, ITokenService tokenService)
        {
            _accountRepo = accountRepository;
            _tokenService = tokenService;
        }
        [Function("Login")]
        public async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "account/login")] HttpRequest request)
        {
            KullaniciLoginDto? loginDto = await JsonSerializer.DeserializeAsync<KullaniciLoginDto>(request.Body);
            if (loginDto == null) return new BadRequestObjectResult("Kullanici adi ve parola bos olamaz!");

            Console.WriteLine(loginDto);

            try{
                var kullanici = await _accountRepo.LoginKullaniciAsync(loginDto);
                return new OkObjectResult(kullanici);

            }catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("Register")]
        public async Task<IActionResult> Register([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "account/register")] HttpRequest request)
        {
            KullaniciRegisterDto? registerDto = await JsonSerializer.DeserializeAsync<KullaniciRegisterDto>(request.Body);
            if (registerDto == null) return new BadRequestObjectResult("Kullanici adi ve parola bos olamaz!");

            try{
                var kullanici = await _accountRepo.RegisterKullaniciAsync(registerDto);
                return new OkObjectResult(kullanici);

            }catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}