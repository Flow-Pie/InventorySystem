using Application.DTO.Response.Order;
using MediatR;

namespace Application.Service.Products.Queries.Orders;

public record GetOrdersByIdQuery(string UserId) : IRequest<IEnumerable<GetOrderResponseDTO>>;
