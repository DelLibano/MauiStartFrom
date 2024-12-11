using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MauiStartFrom.Services
{
    public class XxxWebService
    {
        private readonly HttpClient xxxWS;
        private readonly JsonSerializerOptions serializerOptions;
        private string? JwtToken = string.Empty;

        public XxxWebService()
        {
            xxxWS = new HttpClient();
            xxxWS.BaseAddress = new Uri(Constants.WebServiceAddress);
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        private void AddAuthorizationHeader()
        {
            if (!xxxWS.DefaultRequestHeaders.Contains(Constants.Authorization))
                xxxWS.DefaultRequestHeaders.Add(Constants.Authorization, "Bearer " + JwtToken);
        }

        private void RemoveAuthorizationHeader()
        {
            if (xxxWS.DefaultRequestHeaders.Contains(Constants.Authorization))
                xxxWS.DefaultRequestHeaders.Remove(Constants.Authorization);
        }

        public async Task<string?> Login(string email, string password)
        {
            RemoveAuthorizationHeader();
            var requestBody = JsonSerializer.Serialize(new { email = email, password = password }, serializerOptions);
            try
            {
                var response = await xxxWS.PostAsync(Constants.Login, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.Forbidden)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JwtToken = JsonSerializer.Deserialize<string>(content, serializerOptions);
                }
            }
            catch (HttpRequestException)
            {
                return null;
            }
            return null;
        }

        public async Task<List<Forecast>?> Weather()
        {
            AddAuthorizationHeader();
            try
            {
                return await xxxWS.GetFromJsonAsync<List<Forecast>?>(Constants.Weather);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
