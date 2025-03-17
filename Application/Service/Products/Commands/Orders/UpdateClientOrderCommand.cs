using Application.DTO.Request.Orders;
using Application.DTO.Response;
using MediatR;

namespace Application.Services.Products.Orders;

public record UpdateClientOrderCommand(UpdateClientOrderRequestDTO OrderRequest) : IRequest<ServiceResponse>;