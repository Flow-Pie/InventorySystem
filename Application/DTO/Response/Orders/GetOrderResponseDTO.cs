namespace Application.DTO.Response.Order;
public class GetOrderResponseDTO : OrderBaseDTO
{
    public Guid Id { get; set; }
    public string State { get; set; }
    public string ProductName { get; set; }
    public string SerialNumber { get; set; }
    public DateTime OrderedDate { get; set; }
    public DateTime DeliveringDate { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount => Price * Quantity;
    
    public string ProductImage {get; set;}
    public string ClientId { get; set; }
    public string ClientName { get; set; }
     
}