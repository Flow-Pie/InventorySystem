namespace Application.DTO.Request.Products;
using Application.DTO;

public class GetProductResponseDTO : ProductBaseDTO{
    public Guid Id { get; set; }
    public GetCategoryResponseDTO Category { get; set; }=null;
    public GetLocationResponseDTO Location { get; set; }=null;
    public DateTime DateAdded { get; set; }
}