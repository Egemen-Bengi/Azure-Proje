using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.DTOs.DiziDtos;
using Azure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Azure.Controllers
{
    public class DiziController
    {
        private readonly IDiziRepository _diziRepo;
        private readonly ITokenService _tokenService;
        public DiziController(IDiziRepository diziRepo, ITokenService tokenService)
        {
            _diziRepo = diziRepo;
            _tokenService = tokenService;
        }

        [Function("GetDiziler")]
        public async Task<IActionResult> GetDiziler([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "dizi")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            if(claims["role"] == null)
                return new UnauthorizedObjectResult("Yetkisiz Erisim");

            try
            {
            var diziler = await _diziRepo.GetAllDizilerAsync();

            return new OkObjectResult(diziler);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("GetDiziById")]
        public async Task<IActionResult> GetDiziById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "dizi/{id}")] HttpRequest req, int id)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            if(claims["role"] == null)
                return new UnauthorizedObjectResult("Yetkisiz Erisim");

            try
            {
                var dizi = await _diziRepo.GetDiziByIdAsync(id);

                return new OkObjectResult(dizi);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("DeleteDiziById")]
        public async Task<IActionResult> DeleteDiziById([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "dizi/{id}")] HttpRequest req, int id)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            if(claims["role"] != "Admin")
                return new UnauthorizedObjectResult("Yetkisiz Erisim");

            try
            {
                var dizi = await _diziRepo.DeleteDiziByIdAsync(id);

                return new OkObjectResult(dizi);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("CreateDizi")]
        public async Task<IActionResult> CreateDizi([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "dizi")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            if(claims["role"] != "Admin")
                return new UnauthorizedObjectResult("Yetkisiz Erisim");

            try
            {
                using var reader = new StreamReader(req.Body);
                string requestBody = await reader.ReadToEndAsync();
                DiziCreateDto diziCreateDto = JsonSerializer.Deserialize<DiziCreateDto>(requestBody) ?? throw new Exception("Body parametresi duzgun girilmelidir");

                var dizi = await _diziRepo.CreateDiziAsync(diziCreateDto);

                return new CreatedAtActionResult("GetDiziById", "Dizi", new { id = dizi.Id }, dizi);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }   
        }

        [Function("UpdateDizi")]
        public async Task<IActionResult> UpdateDizi([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "dizi/{id}")] HttpRequest req, int id)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            if(claims["role"] != "Admin")
                return new UnauthorizedObjectResult("Yetkisiz Erisim");

            try
            {
                using var reader = new StreamReader(req.Body);
                string requestBody = await reader.ReadToEndAsync();
                DiziUpdateDto diziUpdateDto = JsonSerializer.Deserialize<DiziUpdateDto>(requestBody) ?? throw new Exception("Body parametresi duzgun girilmelidir");

                var dizi = await _diziRepo.UpdateDiziAsync(id, diziUpdateDto);

                return new OkObjectResult(dizi);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }   
        }
    }
}