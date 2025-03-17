using Application.DTO.Request.Products;
using Application.DTO.Response;
using MediatR;

namespace Application.Products.Commands.Categories;
public record CreateCategoryCommand (AddCategoryRequestDTO CategoryModel) : IRequest<ServiceResponse>;