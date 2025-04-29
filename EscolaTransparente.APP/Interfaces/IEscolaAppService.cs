using EscolaTransparente.Application.Data.DataTransferObjects.Escola;

namespace EscolaTransparente.Application.Interfaces
{
    public interface IEscolaAppService
    {
        Task<EscolaReadDTO?> ObterEscolaPorId(int escolaId);
        Task<EscolaReadDTO> AdicionarEscola(EscolaInsertDTO escola);
    }
}
