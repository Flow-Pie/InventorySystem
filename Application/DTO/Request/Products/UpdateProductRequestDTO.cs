namespace Application.DTO.Request.Products;

public class UpdateProductRequestDTO : ProductBaseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}