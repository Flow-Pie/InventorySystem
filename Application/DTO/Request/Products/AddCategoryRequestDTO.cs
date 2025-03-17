using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Request.Products;

public class AddCategoryRequestDTO 
{
    [Required]
    public string Name { get; set; }
}