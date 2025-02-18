using System.Net.Http;
using EvtSource;

namespace PocketBaseDotnetClient;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public enum RecordAction
{
    Create, Delete, Update
}

public class RealTimeAction<T>
{
    public T record;
    
    public RecordAction action;
}

public class RealTimeCollection<T>(string _uri)
{
    private string uri = _uri;
    
    public event Action<RealTimeAction<T>> OnMessage;
    
    private class Root
    {
        public string clientId { get; set; }
    }

    private bool startup = true;
    
    public void StartListening(string collection)
    {
        if (uri.EndsWith("/"))
            uri = uri.Remove(uri.Length - 1);
     
        var evt = new EventSourceReader(new Uri(uri + "/api/realtime")).Start();
        evt.MessageReceived += async (object sender, EventSourceMessageEventArgs e) =>
        {
            if (startup)
            {

                Root r = JsonConvert.DeserializeObject<Root>(e.Message);

                using var httpClient = new HttpClient();
                var url = uri + "/api/realtime";

                var data = new
                {
                    clientId = r.clientId, 
                    subscriptions = new[] { collection }
                };

                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();
                startup = false;
            }
            else
            {
                var item = JsonConvert.DeserializeObject<RealTimeAction<T>>(e.Message);
                OnMessage?.Invoke(item);
            } 
                
            evt.Disconnected += async (object sender, DisconnectEventArgs e) =>
            {
                await Task.Delay(2000);
                evt.Start(); 
            };
        };
    }
}