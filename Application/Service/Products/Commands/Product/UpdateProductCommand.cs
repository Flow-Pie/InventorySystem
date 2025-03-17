using Application.DTO.Request.Products;
using Application.DTO.Response;
using MediatR;

namespace Application.Products.Commands.Product;

public record UpdateProductCommand(UpdateProductRequestDTO ProductModel) : IRequest<ServiceResponse>;