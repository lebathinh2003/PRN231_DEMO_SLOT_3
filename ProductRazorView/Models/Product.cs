using System.Text.Json.Serialization;

namespace ProductRazorView.Models;

public class Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("price")]
    public float Price { get; set; }

}
