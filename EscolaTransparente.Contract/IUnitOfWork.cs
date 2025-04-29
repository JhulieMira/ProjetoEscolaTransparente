using EscolaTransparente.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Contract
{
    public interface IUnitOfWork
    {
        DbSet<EscolaModel> Escolas { get; }
        DbSet<AvaliacaoModel> Avaliacoes { get; }
        DbSet<CaracteristicaModel> Caracteristicas { get; }
        DbSet<CaracteristicasEscolaModel> CaracteristicasEscola { get; }
        DbSet<ContatoModel> Contatos { get; }
        DbSet<EnderecoModel> Enderecos { get; }
        DbSet<RespostaAvaliacaoModel> RespostasAvaliacao { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
} 