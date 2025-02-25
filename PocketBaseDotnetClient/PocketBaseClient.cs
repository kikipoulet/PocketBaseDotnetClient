using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PocketBaseDotnetClient;

public class PocketBaseClient
{
    
    
    public  HttpClient _httpClient;
    public  PocketBaseAuth Auth; 

    public PocketBaseClient(Uri url)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = url
        };
        
        Auth = new PocketBaseAuth(_httpClient);
    }    
    
    public CollectionQuery Collection(string collectionName)
    {
        return new CollectionQuery(this, collectionName);
    }

    public async Task<ListCollectionResult> ListCollections()
    {
        var url = $"/api/collections";

        ApplyHook();
        
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<ListCollectionResult>(await response.Content.ReadAsStringAsync());
    }
    
    public async Task<LogsResult> ListLogs()
    {
        var url = $"/api/logs";

        ApplyHook();
        
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<LogsResult>(await response.Content.ReadAsStringAsync());
    }
    

    public void ApplyHook()
    {
        if (!string.IsNullOrEmpty(Auth.AuthToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Auth.AuthToken);
        }
    }
}