using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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

        [Function("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "kullanici")] HttpRequest request)
        {
            string? authHeader = request.Headers.Authorization;

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedResult();

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedResult();

            var kullaniciList = await _kullaniciRepo.GetAllKullanicilar();

            return new OkObjectResult(kullaniciList);
        }

        [Function("GetUserById")]
        public async Task<IActionResult> GetUserById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "kullanici/{id}")] HttpRequest request, string id)
        {
            string? authHeader = request.Headers.Authorization;

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedResult();

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedResult();

            try
            {
                var kullanici = await _kullaniciRepo.GetKullaniciById(id);
                return new OkObjectResult(kullanici);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "kullanici/email/{email}")] HttpRequest request, string email)
        {
            string? authHeader = request.Headers.Authorization;

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedResult();

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedResult();

            try
            {
                var kullanici = await _kullaniciRepo.GetKullaniciByEmail(email);
                return new OkObjectResult(kullanici);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("DeleteUserById")]
        public async Task<IActionResult> DeleteUserById([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "kullanici/{id}")] HttpRequest request, string id)
        {
            string? authHeader = request.Headers.Authorization;

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedResult();

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedResult();

            var claims = _tokenService.GetTokenClaims(token);
            
            if (claims["role"] != "Admin")
                return new UnauthorizedObjectResult("Yetkisiz Erisim");

            try
            {
                var kullanici = await _kullaniciRepo.DeleteKullaniciById(id);
                return new OkObjectResult(kullanici);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}