namespace Application.DTO.Response.Orders;

public record GetOrdersCountResponseDTO(int Processing, int Delivering, int Delivered, int Canceled );