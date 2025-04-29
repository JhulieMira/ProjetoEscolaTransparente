using EscolaTransparente.Domain.Entities;

namespace EscolaTransparente.Domain.Interfaces.Services
{
    public interface IEscolaService
    {
        Task<EscolaModel> AdicionarEscola(EscolaModel escola);
    }
}
