using EscolaTransparente.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Infraestructure.Context
{
    public class DataBaseWrapper
    {
        private readonly AppDbContext _context;

        public DbSet<EscolaModel> Escolas => _context.Set<EscolaModel>();
        public DbSet<AvaliacaoModel> Avaliacoes => _context.Set<AvaliacaoModel>();
        public DbSet<CaracteristicaModel> Caracteristicas => _context.Set<CaracteristicaModel>();
        public DbSet<CaracteristicasEscolaModel> CaracteristicasEscola => _context.Set<CaracteristicasEscolaModel>();
        public DbSet<ContatoModel> Contatos => _context.Set<ContatoModel>();
        public DbSet<EnderecoModel> Enderecos => _context.Set<EnderecoModel>();
        public DbSet<RespostaAvaliacaoModel> RespostasAvaliacao => _context.Set<RespostaAvaliacaoModel>();
            
        public DataBaseWrapper(AppDbContext context)
        {
            _context = context;
        }
    }
}

