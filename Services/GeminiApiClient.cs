using API_WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace API_WebApplication1.Services
{
    public class GeminiApiClient
    {

        private readonly HttpClient _httpClient;

        public GeminiApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

        }
    }
}
