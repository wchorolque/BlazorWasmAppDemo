using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorAppDemo.Services;

public class Product
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions options;
    public Product(HttpClient client, JsonSerializerOptions options)
    {
        this.client = client;
        this.options = options;
    }

    public async Task<List<Models.Product>?> Get()
    {
        var response = await this.client.GetAsync("/v1/products");

        return await JsonSerializer.DeserializeAsync<List<Models.Product>?>(await response.Content.ReadAsStreamAsync());
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