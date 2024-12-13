using Microsoft.Maui.Devices.Sensors;

namespace WeatherApp.Services
{
    public class GeolocationService
    {
        public async Task<Location?> GetCurrentLocationAsync()
        {
            try
            {
                // Configuração da solicitação de localização
                var request = new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.High,
                    Timeout = TimeSpan.FromSeconds(10)
                };

                // Obtém a localização atual
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
                    return location;
                }
                else
                {
                    Console.WriteLine("Não foi possível obter a localização.");
                }
            }
            catch (FeatureNotSupportedException)
            {
                Console.WriteLine("Geolocalização não é")
            }
        }
    }
}