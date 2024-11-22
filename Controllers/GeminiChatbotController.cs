using API_WebApplication1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API_WebApplication1.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class GeminiChatbotController : ControllerBase
        {
            private readonly HttpClient _httpClient;
        private readonly IConfiguration configuration;
        private readonly string _apiKey;

        public GeminiChatbotController(HttpClient httpClient, IConfiguration configuration)
            {
            _httpClient = httpClient;
            this.configuration = configuration;
            _apiKey = configuration["GeminiApiKey"];
        }

            [HttpPost("generate-content")]
            public async Task<IActionResult> GenerateContent([FromBody] Models.GeminiResponse responses)
            {
                var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={_apiKey}";

                var payload = new
                {
                    contents = new[]
                    {
                    new
                    {
                        parts = new[]
                        {
                            new { text = responses.Response }
                        }
                    }
                }
                };

                var json = System.Text.Json.JsonSerializer.Serialize(payload);

                var requestContent = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await _httpClient.PostAsync(url, requestContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return Ok(result);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode, "Error calling external API.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }

}
