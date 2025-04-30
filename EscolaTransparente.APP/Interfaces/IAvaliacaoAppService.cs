using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;

namespace EscolaTransparente.Application.Interfaces
{
    public interface IAvaliacaoAppService
    {
        Task<AvaliacaoReadDTO?> ObterAvaliacaoPorId(int avaliacaoId);
        Task<AvaliacaoReadDTO> AdicionarAvaliacao(AvaliacaoInsertDTO avaliacao);
        Task<AvaliacaoReadDTO> AtualizarAvaliacao(int avaliacaoId, AvaliacaoUpdateDTO avaliacao);
        Task<bool> DeletarAvaliacao(int avaliacaoId);
        Task<List<AvaliacaoReadDTO>> AvaliarEscola(List<AvaliacaoInsertDTO> avaliacoes);
    }
} 