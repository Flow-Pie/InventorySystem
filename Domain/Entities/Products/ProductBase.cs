using System.ComponentModel.DataAnnotations;
public class ProductBase
{
    [Key]
    public Guid Id {get; set;}=Guid.NewGuid();
    public string Name {get; set;}
}