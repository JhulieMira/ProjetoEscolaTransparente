using EscolaTransparente.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Infraestructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public DbContext Context { get; }
        public DbSet<EscolaModel> Escolas { get; }
        public DbSet<AvaliacaoModel> Avaliacoes { get; }
        public DbSet<CaracteristicaModel> Caracteristicas { get; }
        public DbSet<CaracteristicasEscolaModel> CaracteristicasEscola { get; }
        public DbSet<ContatoModel> Contatos { get; }
        public DbSet<EnderecoModel> Enderecos { get; }
        public DbSet<RespostaAvaliacaoModel> RespostasAvaliacao { get; }

        void Commit();
        Task CommitAsync();
    }
}
