using Application.DTO.Response.Order;
using MediatR;

namespace Application.Service.Products.Queries.Orders;

public record GetOrdersById(string UserId) : IRequest<IEnumerable<GetOrderResponseDTO>>;
