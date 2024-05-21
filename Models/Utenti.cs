using System.ComponentModel.DataAnnotations;

namespace Bernardi.Luca._5H.Ecommerce.Models;

public class Utenti
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Cognome { get; set; }
    public required string Nickname { get; set; }
    [DataType(DataType.Date)]
    public DateTime DataNascita { get; set; }
    public required string Email { get; set; }
    public int? Numero { get; set; }
    public required string Password { get; set; }
    public required bool Admin { get; set; }
}  