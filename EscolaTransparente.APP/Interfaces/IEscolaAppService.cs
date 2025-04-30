using EscolaTransparente.Application.Data.DataTransferObjects.Escola;

namespace EscolaTransparente.Application.Interfaces
{
    public interface IEscolaAppService
    {
        Task<EscolaReadDTO?> ObterEscolaPorId(int escolaId);
        Task<EscolaReadDTO> AdicionarEscola(EscolaInsertDTO escola);
        Task<EscolaReadDTO> AtualizarEscola(int escolaId, EscolaUpdateDTO escola);
        Task<bool> DeletarEscola(int escolaId);
    }
}
