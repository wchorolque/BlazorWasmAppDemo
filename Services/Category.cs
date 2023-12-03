using System.Text.Json;

namespace BlazorAppDemo.Services;

public class Category : ICategoryService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;
    public Category(HttpClient client)
    {
        this.client = client;
        options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<Models.Category>?> Get()
    {
        var response = await client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Models.Category>>(content, options);
    }
}

public interface ICategoryService
{
    Task<List<Models.Category>> Get();
}