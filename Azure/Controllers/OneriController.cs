using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Azure.Controllers
{
    public class OneriController
    {
        private readonly IOneriService _oneriService;
        private readonly ITokenService _tokenService;

        public OneriController(IOneriService oneriService, ITokenService tokenService)
        {
            _oneriService = oneriService;
            _tokenService = tokenService;
        }

        [Function("GetDiziOnerisi")]
        public async Task<IActionResult> GetDiziOnerisi([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "oneri/dizi")] HttpRequest req)
        {
            return await HandleOneriRequest(req, "dizi");
        }

        [Function("GetFilmOnerisi")]
        public async Task<IActionResult> GetFilmOnerisi([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "oneri/film")] HttpRequest req)
        {
            return await HandleOneriRequest(req, "film");
        }

        private async Task<IActionResult> HandleOneriRequest(HttpRequest req, string type)
        {
            string? authHeader = req.Headers.Authorization;
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlış");

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Geçersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            if (claims["role"] == null)
                return new UnauthorizedObjectResult("Yetkisiz Erişim");

            try
            {
                using var reader = new StreamReader(req.Body);
                string requestBody = await reader.ReadToEndAsync();
                var userPreferences = JsonSerializer.Deserialize<List<string>>(requestBody);

                if (userPreferences == null || userPreferences.Count == 0)
                {
                    return new BadRequestObjectResult("Kullanıcı tercihleri boş olamaz.");
                }

                var result = type == "dizi"
                    ? await _oneriService.GetDiziOnerisiAsync(userPreferences)
                    : await _oneriService.GetFilmOnerisiAsync(userPreferences);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }
    }
}