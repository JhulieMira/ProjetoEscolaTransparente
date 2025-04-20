using EscolaTransparente.Application.Data.DataTransferObjects.Escola;

namespace EscolaTransparente.Application.Interfaces.Services
{
    public interface IEscolaService
    {
        Task<EscolaDTO?> ObterEscolaPorId(int escolaId);
        void AdicionarEscola(EscolaDTO escola);
    }
}
