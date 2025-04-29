using EscolaTransparente.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Contract.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        DbSet<EscolaModel> Escolas { get; }
        DbSet<AvaliacaoModel> Avaliacoes { get; }
        DbSet<CaracteristicaModel> Caracteristicas { get; }
        DbSet<CaracteristicasEscolaModel> CaracteristicasEscola { get; }
        DbSet<ContatoModel> Contatos { get; }
        DbSet<EnderecoModel> Enderecos { get; }
        DbSet<RespostaAvaliacaoModel> RespostasAvaliacao { get; }
        
        void Commit();
        Task CommitAsync();
    }
} 