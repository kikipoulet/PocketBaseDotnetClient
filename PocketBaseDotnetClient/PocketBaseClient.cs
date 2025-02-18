using System;
using System.Net.Http;
using System.Threading.Tasks;

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
        
    
    public PocketBaseQuery Collection(string collectionName)
    {
        return new PocketBaseQuery(this, collectionName);
    }
    

    public void ApplyHook()
    {
        if (!string.IsNullOrEmpty(Auth.AuthToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Auth.AuthToken);
        }
    }
}