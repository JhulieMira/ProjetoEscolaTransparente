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

        public EscolaAppService(IMapper mapper)
        {
            _mapper = mapper;
         
        }
        public async Task<EscolaDTO?> AdicionarEscola(EscolaDTO escola)
        {
            var escolaResult = _escolaService.AdicionarEscola(_mapper.Map<Domain.Entities.EscolaModel>(escola));
            return _mapper.Map<EscolaDTO>(escolaResult);
        }

        public async Task<EscolaDTO?> ObterEscolaPorId(int escolaId)
        {
            var escola = await _escolaService.ObterEscolaPorId(escolaId);
            if (escola is null) return null;

            return _mapper.Map<EscolaDTO>(escola);  
        }
    }
}
