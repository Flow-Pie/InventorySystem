using System.Text.Json.Serialization;

namespace Application.DTO.Request.Products;

public class GetCategoryResponseDTO : UpdateCategoryRequestDTO
{
    [JsonIgnore]
    public virtual ICollection<GetProductResponseDTO> Products { get; set; } = null;
}