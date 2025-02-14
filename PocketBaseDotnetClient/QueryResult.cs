using System.Collections.Generic;

namespace PocketBaseDotnetClient;

public class QueryResult<T>
{
    public List<T> items { get; set; }
    public int page { get; set; }
    public int perPage { get; set; }
    public int totalItems { get; set; }
    public int totalPages { get; set; }
}