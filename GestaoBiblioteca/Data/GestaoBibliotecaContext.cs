using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TesteBiblioteca.Models;
using SolucaoCrea.Models;

namespace GestaoBiblioteca.Data
{
    public class GestaoBibliotecaContext : DbContext
    {
        public GestaoBibliotecaContext (DbContextOptions<GestaoBibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<TesteBiblioteca.Models.Categoria> Categoria { get; set; } = default!;

        public DbSet<TesteBiblioteca.Models.Pessoa>? Pessoa { get; set; }

        public DbSet<TesteBiblioteca.Models.Livro>? Livro { get; set; }

        public DbSet<SolucaoCrea.Models.Movimentacao>? Movimentacao { get; set; }
    }
}
