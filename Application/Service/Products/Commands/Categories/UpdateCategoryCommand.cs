using Application.DTO.Request.Products;
using Application.DTO.Response;
using MediatR;

namespace Application.Service.Products.Commands;

public record UpdateCategoryCommand (UpdateCategoryRequestDTO CategoryModel) : IRequest<ServiceResponse>;