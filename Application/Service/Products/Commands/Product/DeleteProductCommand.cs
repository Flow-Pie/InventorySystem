using Application.DTO.Response;
using MediatR;

namespace Application.Products.Commands.Product;

public record DeleteProductCommand(Guid Id) : IRequest<ServiceResponse>;