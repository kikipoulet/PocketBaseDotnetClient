using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PocketBaseDotnetClient;

public class PocketBaseQuery
{
    private HttpClient _httpClient;
    private PocketBaseClient _Client;
    private string _collection;
    private Dictionary<string, string> _parameters = new();

    public PocketBaseQuery(PocketBaseClient client, string collection)
    {
        _httpClient = client._httpClient;
        _Client = client;
        _collection = collection;
    }

    public PocketBaseQuery Filter(string filter)
    {
        _parameters["filter"] = HttpUtility.UrlEncode(filter);
        return this;
    }

    public PocketBaseQuery Page(int page, int perPage)
    {
        _parameters["page"] = page.ToString();
        _parameters["perPage"] = perPage.ToString();
        return this;
    }

    public async Task<string> GetAsync()
    {
        var queryString = string.Join("&", _parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        var url = $"/api/collections/{_collection}/records?{queryString}";

        _Client.ApplyHook();
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    
    public async Task<QueryResult<T>> GetAsync<T>()
    {
        _Client.ApplyHook();
        var json = await GetAsync();
        return JsonConvert.DeserializeObject<QueryResult<T>>(json);
    }
    
    public async Task<bool> InsertAsync<T>(T data)
    {
        var url = $"/api/collections/{_collection}/records";
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        _Client.ApplyHook();
        var response = await _httpClient.PostAsync(url, content);
        return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
    }
   
}