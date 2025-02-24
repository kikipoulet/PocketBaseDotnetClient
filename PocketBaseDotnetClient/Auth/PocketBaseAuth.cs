
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class PocketBaseAuth
{
    private readonly HttpClient _httpClient;
    public string? AuthToken { get; private set; } 
    public string? UserId { get; private set; }



    public PocketBaseAuth(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> LoginAsync(string email, string password, string authcollection = "users")
    {
        var url = "/api/collections/" + authcollection + "/auth-with-password";
        var json = JsonConvert.SerializeObject(new { identity = email, password });
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        if (!response.IsSuccessStatusCode) return false;

        var responseBody = await response.Content.ReadAsStringAsync();
        var authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseBody);

        if (authResponse != null)
        {
            AuthToken = authResponse.Token;
            UserId = authResponse.Record.Id;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);
            return true;
        }

        return false;
    }

    public bool IsAuthenticated() => !string.IsNullOrEmpty(AuthToken);

    public void Logout()
    {
        AuthToken = null;
        UserId = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    private class AuthResponse
    {
        public string Token { get; set; } = "";
        public UserRecord Record { get; set; } = new UserRecord();
    }

    private class UserRecord
    {
        public string Id { get; set; } = "";
    }
}