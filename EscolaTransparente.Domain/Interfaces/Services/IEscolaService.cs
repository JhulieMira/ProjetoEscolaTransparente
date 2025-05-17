using EscolaTransparente.Domain.Entities;

namespace EscolaTransparente.Domain.Interfaces.Services
{
    public interface IEscolaService
    {
        Task<EscolaModel> ValidarEscola(EscolaModel escola);
        Task<bool> ValidarSeUsuarioPodeAlterarDadosEscola(string escolaIdUsuario, int escolaId);
    }
}
