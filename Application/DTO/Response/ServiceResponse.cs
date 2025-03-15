namespace Application.DTO.Response;

public record ServiceResponse(bool Flag, string Message)
{
    private string v;

    public ServiceResponse(string v) : this(false, v)
    {
    }
}