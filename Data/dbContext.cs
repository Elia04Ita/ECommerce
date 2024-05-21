using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bernardi.Luca._5H.Ecommerce.Models;

    public class dbContext : DbContext
    {
        public dbContext (DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public DbSet<Bernardi.Luca._5H.Ecommerce.Models.Movie> Movie { get; set; } = default!;

public DbSet<Bernardi.Luca._5H.Ecommerce.Models.Utenti> Utenti { get; set; } = default!;

public DbSet<Bernardi.Luca._5H.Ecommerce.Models.Prodotti> Prodotti { get; set; } = default!;

public DbSet<Bernardi.Luca._5H.Ecommerce.Models.Carrello> Carrello { get; set; } = default!;
    }
