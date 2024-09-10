using System.Text.Json.Serialization;

namespace DEMO_PRN231_SLOT_3.Models;

public class Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("price")]
    public float Price { get; set; }

}
