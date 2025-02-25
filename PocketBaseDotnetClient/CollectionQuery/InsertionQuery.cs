
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class InsertionQuery
{
    private CollectionQuery _collectionQuery;
    
    private Dictionary<string, object> _parameters = new();
    
    public InsertionQuery(CollectionQuery collectionQuery)
    {
        _collectionQuery = collectionQuery;
    }

    public InsertionQuery With(string fieldName, object item)
    {
        _parameters.Add(fieldName, item);
        return this;
    }
    
        
    public async Task<bool> InsertAsync()
    {
        var url = $"/api/collections/{_collectionQuery._collection}/records";

        using var form = new MultipartFormDataContent();
        
        foreach (var keyValuePair in _parameters)
        {
            var value = keyValuePair.Value;
            if (value != null)
            {
                if (value.GetType() == typeof(PocketBaseFileUpload))
                {
                    PocketBaseFileUpload fileUpload = (PocketBaseFileUpload)value;
                    if (fileUpload.Stream != null && fileUpload.Stream.Length > 0)
                    {
                        fileUpload.Stream.Position = 0; 

                        var fileContent = new StreamContent(fileUpload.Stream);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                        form.Add(fileContent, keyValuePair.Key, fileUpload.Filename);
                    }
                }
                else
                {
                    form.Add(new StringContent(value.ToString(), Encoding.UTF8), keyValuePair.Key); 
                }
            }
        }
        
        _collectionQuery._Client.ApplyHook();
        var response = await _collectionQuery._httpClient.PostAsync(url, form);
        return response.EnsureSuccessStatusCode().IsSuccessStatusCode;
    }
}