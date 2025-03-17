namespace Application.DTO.Response.Orders;

public class GetProductOrderedByMonthsResponseDTO 
{
    public string MonthName {get; set;}
    public decimal TotalAmount {get; set;}
    public string FormattedTotalAmount => TotalAmount.ToString("#,##0.00");

}