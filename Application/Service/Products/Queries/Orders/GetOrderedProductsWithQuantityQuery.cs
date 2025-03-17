using Application.DTO.Response.Order;
using Application.DTO.Response.Orders;
using MediatR;

namespace Application.Service.Products.Queries.Orders;

public record GetOrderedProductsWithQuantityQuery(string UserId =null) : IRequest<IEnumerable<GetOrderedProductsWithQuantityResponseDTO>>;