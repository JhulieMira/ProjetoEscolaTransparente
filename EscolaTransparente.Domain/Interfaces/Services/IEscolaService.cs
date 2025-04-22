using EscolaTransparente.Domain.Entities;

namespace EscolaTransparente.Domain.Interfaces.Services
{
    public interface IEscolaService
    {
        Task<EscolaModel?> ObterEscolaPorId(int escolaId);
        Task<EscolaModel> AdicionarEscola(EscolaModel escola);
    }
}
