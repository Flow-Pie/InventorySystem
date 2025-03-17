using Application.DTO.Response;
using MediatR;

namespace Application.Services.Products.Orders;

public record CancelOrderCommand(Guid OrderId) : IRequest<ServiceResponse>;
