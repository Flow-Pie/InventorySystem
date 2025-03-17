namespace Application.DTO.Request.Orders;

public class CreateOrdersRequest : OrderBaseDTO
{
    public string ClientId { get; set; }
}