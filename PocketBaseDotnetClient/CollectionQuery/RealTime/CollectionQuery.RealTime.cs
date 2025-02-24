using System;
using PocketBaseDotnetClient;


public partial class CollectionQuery
{
        public RealTimeCollection<T> ListenToChanges<T>( Action<RealTimeAction<T>> OnMessageReceived)
        {
            var url = _Client._httpClient.BaseAddress.ToString();
            var realtimecollection = new RealTimeCollection<T>(url);

            realtimecollection.OnMessage += OnMessageReceived;

            realtimecollection.StartListening(_collection);

            return realtimecollection;
        }
}
