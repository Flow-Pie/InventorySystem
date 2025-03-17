using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Location : ProductBase
    {
        [JsonIgnore]
        public virtual ICollection<Products> Products { get; set; } = null;
    }
}