using Application.DTO.Request.Orders;
using Application.DTO.Response;
using MediatR;

namespace Application.Services.Products.Orders;

public record CreateOrderCommand(CreateOrderRequestDTO OrderRequest) : IRequest<ServiceResponse>;