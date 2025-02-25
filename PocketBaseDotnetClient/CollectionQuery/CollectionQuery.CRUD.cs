
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PocketBaseDotnetClient;


public partial class CollectionQuery
{
    
    public CollectionQuery Filter(string filter)
    {
        _parameters["filter"] = HttpUtility.UrlEncode(filter);
        return this;
    }

    public CollectionQuery Page(int page, int perPage)
    {
        _parameters["page"] = page.ToString();
        _parameters["perPage"] = perPage.ToString();
        return this;
    }
    
    
    
    public CollectionQuery Sort(string sortBy)
    {
        _parameters["sort"] = HttpUtility.UrlEncode(sortBy);
        return this;
    }
    
    public CollectionQuery Expand(string expandBy)
    {
        _parameters["expand"] = HttpUtility.UrlEncode(expandBy);
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
    
    public async Task<CollectionQueryResult<T>> GetAsync<T>()
    {
        _Client.ApplyHook();
        var json = await GetAsync();
        return JsonConvert.DeserializeObject<CollectionQueryResult<T>>(json);
    }

    
    public async Task<bool> InsertAsync<T>(T data)
    {
        var url = $"/api/collections/{_collection}/records";

        using var form = new MultipartFormDataContent();
        
        foreach (var prop in typeof(T).GetProperties())
        {
            var value = prop.GetValue(data);
            if (value != null)
            {
                if (prop.PropertyType == typeof(PocketBaseFileUpload))
                {
                    PocketBaseFileUpload fileUpload = (PocketBaseFileUpload)value;
                    if (fileUpload.Stream != null && fileUpload.Stream.Length > 0)
                    {
                        fileUpload.Stream.Position = 0; 

                        var fileContent = new StreamContent(fileUpload.Stream);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                        form.Add(fileContent, prop.Name, fileUpload.Filename);
                    }
                }
                else
                {
                    form.Add(new StringContent(value.ToString(), Encoding.UTF8), prop.Name); 
                }
            }
        }
        
        _Client.ApplyHook();
        var response = await _httpClient.PostAsync(url, form);
        return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
    }

    public InsertionQuery NewItem()
    {
        return new InsertionQuery(this);
    }
    
}

public class PocketBaseFileUpload
{
    public MemoryStream Stream;
    public string Filename = "data.png";

    public PocketBaseFileUpload(MemoryStream stream)
    {
        Stream = stream;
    }

    public PocketBaseFileUpload(MemoryStream stream, string filename)
    {
        Stream = stream;
        Filename = filename;
    }
}