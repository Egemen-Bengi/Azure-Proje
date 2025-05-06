using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.DTOs.DizilerKullanicilarDtos;
using Azure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Azure.Controllers
{
    public class DiziKullaniciController
    {
        private readonly IDizilerKullanicilarRepository _repository;
        private readonly ITokenService _tokenService;
        public DiziKullaniciController(IDizilerKullanicilarRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;

        }

        [Function("CreateDiziKullanici")]
        public async Task<IActionResult> CreateDiziKullanici([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "dizi-kullanici/{diziId}")] HttpRequest req, int diziId)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            try
            {
                var diziKullaniciCreated = await _repository.CreateDiziVeKullaniciAsync(claims["nameid"], diziId);

                return new CreatedAtActionResult(nameof(GetDiziVeKullaniciByKullaniciId), "DiziKullanici", null, diziKullaniciCreated);
            }
            
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("DeleteDiziKullanici")]
        public async Task<IActionResult> DeleteDiziKullanici([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "dizi-kullanici/{diziId}")] HttpRequest req, int diziId)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            try
            {
                var diziKullaniciDeleted = await _repository.DeleteDiziVeKullaniciByIdAsync(claims["nameid"], diziId);

                return new OkObjectResult(diziKullaniciDeleted);
            }
            
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("GetDiziVeKullaniciByKullaniciId")]
        public async Task<IActionResult> GetDiziVeKullaniciByKullaniciId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "dizi-kullanici/kullanici")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            try
            {
                var diziKullanici = await _repository.GetDiziVeKullaniciByKullaniciIdAsync(claims["nameid"]);

                return new OkObjectResult(diziKullanici);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("GetDiziVeKullaniciByDiziId")]
        public async Task<IActionResult> GetDiziVeKullaniciByDiziId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "dizi-kullanici/dizi/{id}")] HttpRequest req, int id)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            try
            {
                var diziKullanici = await _repository.GetDiziVeKullaniciByDiziIdAsync(id);

                return new OkObjectResult(diziKullanici);
            }
            
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("GetAllDizilerVeKullanicilar")]
        public async Task<IActionResult> GetAllDizilerVeKullanicilar([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "dizi-kullanici")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            try
            {
                var dizilerKullanicilar = await _repository.GetAllDizilerVeKullanicilarAsync();

                return new OkObjectResult(dizilerKullanicilar);
            }
            
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }
    }
}