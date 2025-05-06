using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.DTOs.FilmlerKullanicilarDto;
using Azure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Azure.Controllers
{
    public class FilmKullaniciController
    {
        private readonly IFilmlerKullanicilarRepository _repository;
        private readonly ITokenService _tokenService;

        public FilmKullaniciController(IFilmlerKullanicilarRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        [Function("CreateFilmKullanici")]
        public async Task<IActionResult> CreateFilmKullanici([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "film-kullanici/{filmId}")] HttpRequest req, int filmId)
        {
            string? authHeader = req.Headers.Authorization;
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlış");

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Geçersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            try
            {
                var filmKullaniciCreated = await _repository.CreateFilmVeKullaniciAsync(claims["nameid"], filmId);

                return new CreatedResult($"/api/film-kullanici/kullanici", filmKullaniciCreated);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("DeleteFilmKullanici")]
        public async Task<IActionResult> DeleteFilmKullanici([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "film-kullanici/{filmId}")] HttpRequest req, int filmId)
        {
            string? authHeader = req.Headers.Authorization;
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlış");

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Geçersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            try
            {
                var filmKullaniciDeleted = await _repository.DeleteFilmVeKullaniciByIdAsync(claims["nameid"], filmId);

                return new OkObjectResult(filmKullaniciDeleted);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("GetFilmVeKullaniciByKullaniciId")]
        public async Task<IActionResult> GetFilmVeKullaniciByKullaniciId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "film-kullanici/kullanici")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlış");

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Geçersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            try
            {
                var filmKullanici = await _repository.GetFilmVeKullaniciByKullaniciIdAsync(claims["nameid"]);

                return new OkObjectResult(filmKullanici);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("GetFilmVeKullaniciByFilmId")]
        public async Task<IActionResult> GetFilmVeKullaniciByFilmId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "film-kullanici/film/{id}")] HttpRequest req, int id)
        {
            string? authHeader = req.Headers.Authorization;
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlış");

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Geçersiz Token");

            try
            {
                var filmKullanici = await _repository.GetFilmVeKullaniciByFilmIdAsync(id);

                return new OkObjectResult(filmKullanici);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("GetAllFilmlerVeKullanicilar")]
        public async Task<IActionResult> GetAllFilmlerVeKullanicilar([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "film-kullanici")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlış");

            string token = authHeader["Bearer ".Length..].Trim();

            if (!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Geçersiz Token");

            try
            {
                var filmlerKullanicilar = await _repository.GetAllFilmlerVeKullanicilarAsync();

                return new OkObjectResult(filmlerKullanicilar);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }
    }
}