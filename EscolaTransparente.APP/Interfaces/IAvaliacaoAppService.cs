using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;
using EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica;

namespace EscolaTransparente.Application.Interfaces
{
    public interface IAvaliacaoAppService
    {
        Task<AvaliacaoReadDTO?> ObterAvaliacaoPorId(int avaliacaoId);
        Task<AvaliacaoReadDTO> AdicionarAvaliacao(AvaliacaoInsertDTO avaliacao);
        Task<AvaliacaoReadDTO> AtualizarAvaliacao(int avaliacaoId, AvaliacaoUpdateDTO avaliacao);
        Task<bool> DeletarAvaliacao(int avaliacaoId);
        Task<List<AvaliacaoReadDTO>> AvaliarEscola(List<AvaliacaoInsertDTO> avaliacoes);
        Task<List<AvaliacaoReadDTO?>> ObterAvaliacoesPorEscolaId(int escolaId);
        Task<List<CaracteristicaReadDTO>> ObterCaracteristicasEscolaPorEscolaId(int escolaId);
        Task<CaracteristicaReadDTO> AdicionarCaracteristica(CaracteristicaInsertDTO caracteristica);
        Task<CaracteristicaReadDTO> AdicionarCaracteristicaEscola(CaracteristicaEscolaInsertDTO caracteristicaEscola);
    }
} 