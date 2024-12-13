using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Services
{
    public class WeatherService
    {
        private HttpClient _httpClient;
        private City City;
        private JsonSerializerOptions _jsonSerializerOptions;
        private string apiKey = "9f7dd0192630755d4238b60886a88d84";

        public WeatherService()
        {
            _httpClient = new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<City> GetCityAsync(string cityName)
        {
            Uri uri = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid={apiKey}&lang=pt_br");
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    City = JsonSerializer.Deserialize<City>(content, _jsonSerializerOptions);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"\tERROR {0}", e.Message);
            }
            return City;
        }

        public string GetLocalIconPath(string iconCode)
        {
            var iconMap = new Dictionary<string, string>
    {
        { "01d", "clear_day.png" },
        { "01n", "clear_night.png" },
        { "02d", "partly_cloudy_day.png" },
        { "02n", "partly_cloudy_night.png" },
        { "03d", "cloudy.png" },
        { "03n", "cloudy.png" },
        { "04d", "overcast.png" },
        { "04n", "overcast.png" },
        { "09d", "rain.png" },
        { "09n", "rain.png" },
        { "11d", "storm.png" },
        { "11n", "storm.png" },
        { "13d", "snow.png" },
        { "13n", "snow.png" },
    };

            return iconMap.TryGetValue(iconCode, out var localPath) ? $"resource://Weather.Resources.Images.{localPath}" : "resource://Weather.Resources.Images.default.png";
        }
    }
}