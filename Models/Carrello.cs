using System.ComponentModel.DataAnnotations;

namespace Bernardi.Luca._5H.Ecommerce.Models;

public class Carrello
{
    public int id { get; set; }
    public int Prodotto { get; set; }
    public int Utente { get; set; }
    public int Quantita { get; set; }
}  