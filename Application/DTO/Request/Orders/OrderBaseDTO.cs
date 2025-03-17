using System.ComponentModel.DataAnnotations;
using NetcodeHub.Packages.Extensions.Attributes.RequiredGuid;

namespace Application.DTO.Response.Orders;

public class OrderBaseDTO
{
    [NetcodeHubRequiredGuid(ErrorMessage ="Product must be selected")]
    public Guid ProductId { get; set; }
    [Required]
    [Range(1,int.MaxValue)]
    public int Quantity { get; set; }
}