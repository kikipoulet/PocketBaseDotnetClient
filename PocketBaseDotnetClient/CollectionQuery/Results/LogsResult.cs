using System.Collections.Generic;

namespace PocketBaseDotnetClient;

public class Data
{
    public string auth { get; set; }
    public object details { get; set; }
    public string error { get; set; }
    public double execTime { get; set; }
    public string method { get; set; }
    public string referer { get; set; }
    public string remoteIP { get; set; }
    public int status { get; set; }
    public string type { get; set; }
    public string url { get; set; }
    public string userAgent { get; set; }
    public string userIP { get; set; }
}

public class Log
{
    public string id { get; set; }
    public string created { get; set; }
    public Data data { get; set; }
    public string message { get; set; }
    public int level { get; set; }
}

public class LogsResult
{
    public List<Log> items { get; set; }
    public int page { get; set; }
    public int perPage { get; set; }
    public int totalItems { get; set; }
    public int totalPages { get; set; }
}