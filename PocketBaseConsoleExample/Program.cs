
using PocketBaseConsoleExample;
using PocketBaseDotnetClient;


var pb = new PocketBaseClient(new Uri("http://127.0.0.1:8090"));

bool isLogged = await pb.Auth.LoginAsync("test@test.com", "testtest");

bool success = await pb.Collection("posts").InsertAsync(new Post(){message = "createdtest"});

string resultRawJson = await pb.Collection("posts").GetAsync();

QueryResult<Post> result = await pb.Collection("posts").GetAsync<Post>();
result.items.ForEach(post => Console.WriteLine(post.message));

result = await pb.Collection("posts")
                 .Page(1,10) 
                 .GetAsync<Post>();

result = await pb.Collection("posts")
                 .Filter("message ?~ 'test'") 
                 .GetAsync<Post>();







