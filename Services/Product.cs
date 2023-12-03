using System.Dynamic;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorAppDemo.Services;

public class Product : IProductService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;
    public Product(HttpClient client)
    {
        this.client = client;
        options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<Models.Product>?> Get()
    {
        var response = await this.client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }

        return JsonSerializer.Deserialize<List<Models.Product>>(content, options);
    }

    public async Task Add(Models.Product product)
    {
        var response = await client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task Delete(int productId)
    {
        var response = await client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}

public interface IProductService
{
    Task<List<Models.Product>?> Get();
    Task Add(Models.Product product);
    Task Delete(int productId);
}