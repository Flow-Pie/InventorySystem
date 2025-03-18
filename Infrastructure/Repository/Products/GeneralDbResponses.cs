using Application.DTO.Response;

namespace Infrastructure.Repository.Products;

public static class GeneralDbResponses
{
    public static ServiceResponse ItemAlreadyExists(string item) => new ServiceResponse(false, $"{item} already exists");
    public static ServiceResponse ItemNotFound(string item) => new ServiceResponse(false, $"{item} not found");
    public static ServiceResponse ItemCreated(string item) => new ServiceResponse(true, $"{item} created");
    public static ServiceResponse ItemUpdate(string item) => new ServiceResponse(true, $"{item} updated");
    public static ServiceResponse ItemDelete(string item) => new ServiceResponse(true, $"{item} deleted");

}