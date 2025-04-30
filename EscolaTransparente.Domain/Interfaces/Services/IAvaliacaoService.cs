using EscolaTransparente.Domain.Entities;

namespace EscolaTransparente.Domain.Interfaces.Services
{
    public interface IAvaliacaoService
    {
        Task<AvaliacaoModel> ValidarAvaliacao(AvaliacaoModel avaliacao);
        Task ValidarListaAvaliacoes(List<AvaliacaoModel> avaliacoes);
    }
} 