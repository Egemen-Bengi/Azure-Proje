using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using Azure.DTOs.KullaniciDtos;
using Azure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Azure.Controllers
{
    public class KullaniciController
    {
        private readonly IKullaniciRepository _kullaniciRepo;
        private readonly ITokenService _tokenService;
        public KullaniciController(IKullaniciRepository kullaniciRepo, ITokenService tokenService)
        {
            _kullaniciRepo = kullaniciRepo;
            _tokenService = tokenService;
        }
        [Function("Login")]
        public async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "kullanici/login")] HttpRequest request)
        {
            string? authHeader = request.Headers["Authorization"];
            if(string.IsNullOrEmpty(authHeader) || authHeader.StartsWith("Bearer "))
                return new UnauthorizedResult();

            string token = authHeader.Substring("Bearer ".Length).Trim();
            
            if(_tokenService.ValidateToken(token) == false) return new UnauthorizedResult();

            KullaniciLoginDto? loginDto = await JsonSerializer.DeserializeAsync<KullaniciLoginDto>(request.Body);
            if (loginDto == null) return new BadRequestObjectResult("Kullanici adi ve parola bos olamaz!");

            try{
                var kullanici = await _kullaniciRepo.LoginKullaniciAsync(loginDto);
                return new OkObjectResult(kullanici);

            }catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("Register")]
        public async Task<IActionResult> Register([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "kullanici/register")] HttpRequest request)
        {
            KullaniciRegisterDto? registerDto = await JsonSerializer.DeserializeAsync<KullaniciRegisterDto>(request.Body);
            if (registerDto == null) return new BadRequestObjectResult("Kullanici adi ve parola bos olamaz!");

            try{
                var kullanici = await _kullaniciRepo.RegisterKullaniciAsync(registerDto);
                return new OkObjectResult(kullanici);

            }catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}