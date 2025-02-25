using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using PocketBaseDotnetClient;



public partial class CollectionQuery
{
    protected internal HttpClient _httpClient;
    protected internal PocketBaseClient _Client;
    protected internal string _collection;
    private Dictionary<string, string> _parameters = new();

    public CollectionQuery(PocketBaseClient client, string collection)
    {
        _httpClient = client._httpClient;
        _Client = client;
        _collection = collection;
    }
    
   
}