using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Domain.Interfaces.Services;

namespace EscolaTransparente.Domain.Services
{
    public class EscolaService : IEscolaService
    {

        public EscolaService()
        {
        }

        public async Task<EscolaModel> AdicionarEscola(EscolaModel escola)
        {
            //TO DO: Implementar lógica de validação da entidade
            return escola;
        }
    }
}
