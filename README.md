Simple C# Client for PocketBase. WIP.

PocketBase is weirdly pleasant to work with, so I focus on giving as simple as possible APIs in this library.

Right now, this library can : 
- Auth with mail & password
- List Collections
- List Items in a Collection, and use Filters
- Insert Item in a Collection
- List Logs
- Use Real-Time to listen changes in a Collection

Probably more to come obviously, especially on CRUD.

<br/>

# Auth

<br/>

```csharp

var pb = new PocketBaseClient(new Uri("http://127.0.0.1:8090"));

bool isLogged = await pb.Auth.LoginAsync("test@test.com", "testtest");
```

<br/>

### Auth in a custom collection

```csharp
await pb.Auth.LoginAsync("superuser@test.com", "testtesttest", "_superusers");

```

<br/>

# Collections

<br/>

<details>
  <summary>Details about the collection used in these examples.</summary>
<br/><br/>
To be especially exhaustive, here is the collection "posts" in my PocketBase instance :

![{8C290699-8066-4540-9E52-96B244F68A14}](https://github.com/user-attachments/assets/d8c975dd-b4b0-4fb3-a006-22d1114a7646)

<br/>

And the C# class 'Post' that I use in these examples : 

```csharp
public class Post
{
        public PocketBaseFileUpload attachment { get; set; } 
        
        public string message { get; set; }
}
```
</details>
<br/>

## List Items

<br/>

```csharp
QueryResult<Post> result = await pb.Collection("posts").GetAsync<Post>();

result.items.ForEach(post => Console.WriteLine(post.message));
```

<br/>

### Filters

```csharp
QueryResult<Post> result = await pb.Collection("posts")
                                   .Filter("message ?~ 'test'")
                                   .Page(1,10) 
                                   .GetAsync<Post>();
```

<br/>


#### RAW Json Output

```csharp
 string resultRawJson = await pb.Collection("posts").GetAsync();

```

<br/>

## Insert Item

<br/>

```csharp

bool success = await pb.Collection("posts").InsertAsync(new Post(){message = "simple", attachment = new PocketBaseFileUpload(new MemoryStream(File.ReadAllBytes(@"C:\2a.png")), "image.png")}); 

```

<br/>

### Fluent API Method

```csharp
await pb.Collection("posts")
             .NewItem()
             .With("message", "fluent method")
             .With("attachment",  new PocketBaseFileUpload(new MemoryStream(File.ReadAllBytes(@"C:\2a.png"))))
             .InsertAsync();
```


<br/>

### List Collections 

<br/>

```csharp
var collections = await pb.ListCollections();

foreach (var collection in collections.items)
    Console.WriteLine(collection.name);
```

NB : You probably need to log in as superuser to do that.

<br/>

# Real-Time

<br/>

### Using PocketBaseClient

```csharp

pb.Collection("posts").ListenToChanges<Post>(realtimeaction => Console.WriteLine(realtimeaction.action) );
```


<br/>

### Alternative

```csharp
var realtimecollection = new RealTimeCollection<Post>("http://127.0.0.1:8090");

realtimecollection.OnMessage += action =>
{
    Console.WriteLine(action.record.message);
};

realtimecollection.StartListening("posts");
```

<br/>

# Logs

<br/>

```csharp
var logs = await pb.ListLogs();

foreach (var log in logs.items)
    Console.WriteLine(log.message);
```


