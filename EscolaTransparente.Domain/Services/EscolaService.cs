using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Domain.Interfaces.Repositories;
using EscolaTransparente.Domain.Interfaces.Services;

namespace EscolaTransparente.Domain.Services
{
    public class EscolaService : IEscolaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEscolaRepository _escolaRepository;

        public EscolaService(IUnitOfWork unitOfWork, IEscolaRepository escolaRepository)
        {
            _escolaRepository = escolaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EscolaModel> AdicionarEscola(EscolaModel escola)
        {
            await _escolaRepository.AddAsync(escola);
            await _unitOfWork.CommitAsync();

            return escola;
        }

        public Task<EscolaModel?> ObterEscolaPorId(int escolaId)
        {
            throw new NotImplementedException();
        }
    }
}
