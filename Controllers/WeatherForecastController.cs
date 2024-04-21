using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Weather2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public WeatherForecastController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        [EnableCors("AllowOrigin")] // Enable CORS for this endpoint
        public async Task<IActionResult> GetWeather(string city)
        {
            try
            {
                if (string.IsNullOrEmpty(city))
                {
                    return BadRequest("City name is required.");
                }

                // Replace "YOUR_API_KEY" with your actual API key
                string apiKey = "da31bc09c0fa945e6acfe91b6b52e2c6";
                string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&APPID={apiKey}&units=metric";


                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    // Handle API error
                    return StatusCode((int)response.StatusCode, "Failed to fetch weather data.");
                }
            }
            catch (Exception ex)
            {
                // Handle internal server error
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}

