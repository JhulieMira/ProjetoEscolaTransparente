using AutoMapper;
using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Interfaces.Services;
using EscolaTransparente.Domain.Interfaces.Services;

namespace EscolaTransparente.Application.Services
{
    public class EscolaAppService : IEscolaAppService
    {
        private readonly IMapper _mapper;
        private readonly IEscolaService _escolaService;

        public EscolaAppService(IMapper mapper, IEscolaService escolaService)
        {
            _mapper = mapper;
            _escolaService = escolaService;
        }

        public async Task<EscolaReadDTO?> AdicionarEscola(EscolaReadDTO escola)
        {
            var escolaModel = _mapper.Map<Domain.Entities.EscolaModel>(escola);
            var escolaResult = await _escolaService.AdicionarEscola(escolaModel);
            return _mapper.Map<EscolaReadDTO>(escolaResult);
        }

        public async Task<EscolaReadDTO?> ObterEscolaPorId(int escolaId)
        {
            var escola = await _escolaService.ObterEscolaPorId(escolaId);
            if (escola is null) return null;

            return _mapper.Map<EscolaReadDTO>(escola);  
        }
    }
}
