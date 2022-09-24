using Ecommerce_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_1.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        { }

        public DbSet<Usuario> Usuario { get; set; }

    }
}
