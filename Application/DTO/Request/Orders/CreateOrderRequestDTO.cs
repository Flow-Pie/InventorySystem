namespace Application.DTO.Request.Orders;

public class CreateOrderRequestDTO : OrderBaseDTO
{
    public string ClientId { get; set; }
}