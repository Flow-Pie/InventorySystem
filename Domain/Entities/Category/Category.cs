using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Category : ProductBase
    {
        [JsonIgnore]
        public ICollection<Products> Products { get; set; } = null;
    }
}