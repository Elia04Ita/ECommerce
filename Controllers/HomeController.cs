using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bernardi.Luca._5H.Ecommerce.Models;

namespace Bernardi.Luca._5H.Ecommerce.Controllers;

public class HomeController : Controller
{

    private readonly dbContext _context;

    public HomeController(dbContext context)
    {
        _context = context;
    }


    //seleziona gli ultimi arrivi per data e id e anche i prodotti futuri per data
    public IActionResult Index()
    {
        DateTime today = DateTime.Today;

        var latestProducts = _context.Prodotti
            .Where(p => p.ReleaseDate <= today)
            .OrderByDescending(p => p.ReleaseDate)
            .ThenByDescending(p => p.Id)
            .Take(3)
            .ToList();

        var futureProducts = _context.Prodotti
            .Where(p => p.ReleaseDate > today)
            .OrderBy(p => p.ReleaseDate)
            .ToList();
    
        var allProducts = _context.Prodotti
            .Where(p => p.ReleaseDate <= today)
            .ToList();

        var viewModel = new Tuple<List<Prodotti>, List<Prodotti>, List<Prodotti>>(latestProducts, futureProducts, allProducts);
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
