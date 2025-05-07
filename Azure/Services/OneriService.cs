using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Azure.Services
{
    public class OneriService : IOneriService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OneriService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetDiziOnerisiAsync(List<string> userPreferences)
        {
            return await GetOneriAsync(userPreferences, "dizi");
        }

        public async Task<string> GetFilmOnerisiAsync(List<string> userPreferences)
        {
            return await GetOneriAsync(userPreferences, "film");
        }

        private async Task<string> GetOneriAsync(List<string> userPreferences, string type)
        {
            var apiKey = type == "dizi"
                ? _configuration["AzureOpenAI:DiziApiKey"]
                : _configuration["AzureOpenAI:FilmApiKey"];

            var endpoint = type == "dizi"
                ? _configuration["AzureOpenAI:DiziEndpoint"]
                : _configuration["AzureOpenAI:FilmEndpoint"];

            var deployment = type == "dizi"
                ? _configuration["AzureOpenAI:DiziDeploymentName"]
                : _configuration["AzureOpenAI:FilmDeploymentName"];

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(deployment))
            {
                throw new Exception($"Azure OpenAI yapılandırması eksik! ({type})");
            }

            // BaseAddress ayarını yap
            _httpClient.BaseAddress = new Uri(endpoint);

            var prompt = $"Kullanıcının tercihleri: {string.Join(", ", userPreferences)}. Bu kullanıcıya 3 tane {type} öner.";
            var requestBody = new
            {
                model = deployment,
                messages = new[]
                {
                    new { role = "system", content = $"Sen bir {type} önerme asistanısın. Kullanıcının tercihleri doğrultusunda ona {type} öner. Önerilerini bir json dosyası gibi yaz. Örnek: {{ \"oneri1\":{{ \"{type}_adi\": \"ad\", \"{type}_aciklamasi\": \"aciklama\" }} }} " },
                    new { role = "user", content = prompt }
                },
                max_tokens = 800,
                temperature = 0.7,
                top_p = 0.95,
                frequency_penalty = 0,
                presence_penalty = 0
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var response = await _httpClient.PostAsync($"/openai/deployments/{deployment}/chat/completions?api-version=2025-01-01-preview", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Azure OpenAI API çağrısı başarısız oldu: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseJson = JsonSerializer.Deserialize<JsonElement>(responseContent);

            return responseJson.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? $"{type} önerisi alınamadı.";
        }
    }
}