using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.DTOs.RolDtos;
using Azure.Interfaces;
using Azure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Azure.Controllers
{
    public class RolController
    {
        private readonly IRollerRepository _rollerRepo;
        private readonly ITokenService _tokenService;
        public RolController(IRollerRepository rollerRepo, ITokenService tokenService)
        {
            _rollerRepo = rollerRepo;
            _tokenService = tokenService;
        }

        [Function("GetRoller")]
        public async Task<IActionResult> GetRoller([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rol")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            try
            {
                var roller = await _rollerRepo.GetRollerAsync();

                return new OkObjectResult(roller);
            }
            catch(Exception)
            {
                return new BadRequestObjectResult("Roller alinirken bir sorun olustu!");
            }
        }

        [Function("GetRolById")]
        public async Task<IActionResult> GetRolById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rol/{id}")] HttpRequest req, int id)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            try
            {
                var rol = await _rollerRepo.GetRolByIdAsync(id);

                return new OkObjectResult(rol);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("DeleteRolById")]
        public async Task<IActionResult> DeleteRolById([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "rol/{id}")] HttpRequest req, int id)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            try
            {
                var rol = await _rollerRepo.DeleteRolByIdAsync(id);

                return new OkObjectResult(rol);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("CreateRol")]
        public async Task<IActionResult> CreateRol([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "rol")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");
            
            try
            {
                using var reader = new StreamReader(req.Body);
                string requestBody = await reader.ReadToEndAsync();
                RolCreateDto rolCreateDto = JsonSerializer.Deserialize<RolCreateDto>(requestBody) ?? throw new Exception("Body parametresi duzgun girilmelidir");

                var addedRol = await _rollerRepo.CreateRolAsync(rolCreateDto);

                return new OkObjectResult(addedRol);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [Function("UpdateRol")]
        public async Task<IActionResult> UpdateRol([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "rol")] HttpRequest req)
        {
            string? authHeader = req.Headers.Authorization;
            if(string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return new UnauthorizedObjectResult("Token yok veya yanlis");

            string token = authHeader["Bearer ".Length..].Trim();

            if(!_tokenService.ValidateToken(token))
                return new UnauthorizedObjectResult("Gecersiz Token");

            try
            {
                using var reader = new StreamReader(req.Body);
                string requestBody = await reader.ReadToEndAsync();
                RolUpdateDto rolUpdateDto = JsonSerializer.Deserialize<RolUpdateDto>(requestBody) ?? throw new Exception("Body parametresi duzgun girilmelidir");

                var updatedRol = await _rollerRepo.UpdateRolAsync(rolUpdateDto);

                return new OkObjectResult(updatedRol);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}