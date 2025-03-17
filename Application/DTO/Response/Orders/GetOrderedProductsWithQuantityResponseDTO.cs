namespace Application.DTO.Response.Orders;

public class GetOrderedProductsWithQuantityResponseDTO
{
    public string ProductName { get; set; }
    public int QuantityOrdered { get; set; }
}