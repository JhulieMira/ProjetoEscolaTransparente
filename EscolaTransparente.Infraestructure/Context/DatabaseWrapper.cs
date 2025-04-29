using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscolaTransparente.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Infraestructure.Context
{
    public class DatabaseWrapper
    {
        private readonly AppDbContext _context;

        public DbSet<EscolaModel> Escolas => _context.Set<EscolaModel>();
        public DbSet<AvaliacaoModel> Avaliacoes => _context.Set<AvaliacaoModel>();

        public int MyProperty { get; set; }

        public DatabaseWrapper(AppDbContext context)
        {
            _context = context;
            var test = _context.Set<EscolaModel>();
        }
    }
}
