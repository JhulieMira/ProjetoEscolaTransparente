using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Domain.Interfaces.Repositories;
using EscolaTransparente.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public DbContext Context => _context;

        public DbSet<EscolaModel> Escolas => _context.Set<EscolaModel>();
        public DbSet<AvaliacaoModel> Avaliacoes => _context.Set<AvaliacaoModel>();
        public DbSet<CaracteristicaModel> Caracteristicas => _context.Set<CaracteristicaModel>();
        public DbSet<CaracteristicasEscolaModel> CaracteristicasEscola => _context.Set<CaracteristicasEscolaModel>();
        public DbSet<ContatoModel> Contatos => _context.Set<ContatoModel>();
        public DbSet<EnderecoModel> Enderecos => _context.Set<EnderecoModel>();
        public DbSet<RespostaAvaliacaoModel> RespostasAvaliacao => _context.Set<RespostaAvaliacaoModel>();

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }   

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
