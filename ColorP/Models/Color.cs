using System.ComponentModel.DataAnnotations;

public class Color
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsInStock { get; set; }
    public string HexCode { get; set; } = string.Empty;
}
