using System.ComponentModel.DataAnnotations;

namespace Bernardi.Luca._5H.Ecommerce.Models;

public class Prodotti
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageFileName { get; set; }
    public string Image
        {
            get
            {
                return $"{ImageFileName}";
            }
        }
}  