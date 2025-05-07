using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.DTOs.FilmDtos;
using Azure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Azure.Controllers
{
    public class FilmController
    {
        private readonly IFilmRepository _filmRepo;
        private readonly ITokenService _tokenService;

        public FilmController(IFilmRepository filmRepo, ITokenService tokenService)
        {
            _filmRepo = filmRepo;
            _tokenService = tokenService;
        }

        [Function("GetFilmler")]
        public async Task<IActionResult> GetFilmler([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "film")] HttpRequest req)
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
                var filmler = await _filmRepo.GetAllFilmlerAsync();
                return new OkObjectResult(filmler);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("GetFilmById")]
        public async Task<IActionResult> GetFilmById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "film/{id}")] HttpRequest req, int id)
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
                var film = await _filmRepo.GetFilmByIdAsync(id);
                return new OkObjectResult(film);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("DeleteFilmById")]
        public async Task<IActionResult> DeleteFilmById([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "film/{id}")] HttpRequest req, int id)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            if(claims["role"] == "Admin")
                return new UnauthorizedObjectResult("Yetkisiz Erisim");

            try
            {
                var film = await _filmRepo.DeleteFilmByIdAsync(id);
                return new OkObjectResult(film);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("CreateFilm")]
        public async Task<IActionResult> CreateFilm([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "film")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            if(claims["role"] == "Admin")
                return new UnauthorizedObjectResult("Yetkisiz Erisim");

            try
            {
                using var reader = new StreamReader(req.Body);
                string requestBody = await reader.ReadToEndAsync();
                var dto = JsonSerializer.Deserialize<FilmCreateDto>(requestBody);

                if (dto == null)
                    throw new Exception("Geçersiz veri!");

                var film = await _filmRepo.CreateFilmAsync(dto);
                return new CreatedResult($"/api/film/{film.Id}", film);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }

        [Function("UpdateFilm")]
        public async Task<IActionResult> UpdateFilm([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "film/{id}")] HttpRequest req, int id)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            var claims = _tokenService.GetTokenClaims(token);

            if(claims["role"] == "Admin")
                return new UnauthorizedObjectResult("Yetkisiz Erisim");

            try
            {
                using var reader = new StreamReader(req.Body);
                string requestBody = await reader.ReadToEndAsync();
                var dto = JsonSerializer.Deserialize<FilmUpdateDto>(requestBody);

                if (dto == null)
                    throw new Exception("Geçersiz veri!");

                var film = await _filmRepo.UpdateFilmAsync(id, dto);
                return new OkObjectResult(film);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hata: {ex.Message}");
            }
        }
    }
}