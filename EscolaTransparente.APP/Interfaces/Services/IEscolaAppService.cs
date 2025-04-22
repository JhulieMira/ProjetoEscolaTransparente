using EscolaTransparente.Application.Data.DataTransferObjects.Escola;

namespace EscolaTransparente.Application.Interfaces.Services
{
    public interface IEscolaAppService
    {
        Task<EscolaDTO?> ObterEscolaPorId(int escolaId);
        Task<EscolaDTO> AdicionarEscola(EscolaDTO escola);
    }
}
