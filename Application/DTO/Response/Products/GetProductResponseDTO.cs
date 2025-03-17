namespace Application.DTO.Request.Products;

public class GetProductResponseDTO : ProductBaseDTO{
    public Guid Id { get; set; }
    public GetCategoryResponseDTO Category { get; set; }=null;
    public GetLocationResponseDTO Location { get; set; }=null;
    public DateTime dateAdded { get; set; }
}