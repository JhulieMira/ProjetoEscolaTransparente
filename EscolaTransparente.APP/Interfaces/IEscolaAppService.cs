using EscolaTransparente.Application.Data.DataTransferObjects.Escola;

namespace EscolaTransparente.Application.Interfaces
{
    public interface IEscolaAppService
    {
        Task<EscolaDetalhadaReadDTO?> ObterEscolaPorId(int escolaId);
        Task<EscolaDetalhadaReadDTO> AdicionarEscola(EscolaInsertDTO escola);
        Task<EscolaDetalhadaReadDTO> AtualizarEscola(int escolaId, EscolaUpdateDTO escola);
        Task<bool> DeletarEscola(int escolaId);
        Task<List<EscolaDetalhadaReadDTO>> ObterTop10EscolasAsync();
    }
}
