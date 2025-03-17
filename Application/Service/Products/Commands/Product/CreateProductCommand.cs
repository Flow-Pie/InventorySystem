using Application.DTO.Request.Products;
using Application.DTO.Response;
using MediatR;

namespace Application.Products.Commands.Product
{
    public record CreateProductCommand(AddProductRequestDTO ProductModel) : IRequest<ServiceResponse>;
}