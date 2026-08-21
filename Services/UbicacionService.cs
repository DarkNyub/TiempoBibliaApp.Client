using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace TiempoBiblia.Client.Services
{
    public class UbicacionService
    {
        private readonly HttpClient _http;
        
        // Asumimos Colombia por defecto por si el cliente tiene bloqueadores de red
        public bool EsColombia { get; private set; } = true; 
        public bool Inicializado { get; private set; } = false;

        public UbicacionService(HttpClient http)
        {
            _http = http;
        }

        public async Task DetectarUbicacionAsync()
        {
            if (Inicializado) return;

            try
            {
                // Llamamos a una API gratuita y súper rápida
                var response = await _http.GetFromJsonAsync<GeoRespuesta>("https://api.country.is/");
                EsColombia = response?.Country == "CO";
            }
            catch
            {
                EsColombia = true; // Fallback seguro
            }
            finally
            {
                Inicializado = true;
            }
        }

        private class GeoRespuesta
        {
            [JsonPropertyName("country")]
            public string Country { get; set; } = string.Empty;
        }
    }
}