using Application.DTO.Response.Orders;
using MediatR;

namespace Application.Service.Products.Queries.Orders;

public record GetProductOrderedByMonthsQuery(string UserId = null) : IRequest<IEnumerable<GetProductOrderedByMonthsResponseDTO>>;