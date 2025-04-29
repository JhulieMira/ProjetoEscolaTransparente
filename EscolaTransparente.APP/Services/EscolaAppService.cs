using AutoMapper;
using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Interfaces;
using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Application.Services
{
    public class EscolaAppService : IEscolaAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EscolaAppService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<EscolaReadDTO?> AdicionarEscola(EscolaInsertDTO escola)
        {
            var escolaModel = _mapper.Map<EscolaModel>(escola);
            await _unitOfWork.Escolas.AddAsync(escolaModel);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<EscolaReadDTO>(escolaModel);
        }

        public async Task<EscolaReadDTO?> ObterEscolaPorId(int escolaId)
        {
            var escola = await _unitOfWork.Escolas
                .Include(e => e.Contato)
                .Include(e => e.Endereco)
                .Include(e => e.CaracteristicasEscola)
                    .ThenInclude(ce => ce.Caracteristica)
                .FirstOrDefaultAsync(e => e.EscolaId == escolaId);

            if (escola is null) 
                return null;

            return _mapper.Map<EscolaReadDTO>(escola);
        }
    }
}
