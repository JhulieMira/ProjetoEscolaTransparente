using AutoMapper;
using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Interfaces;
using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Domain.Interfaces.Services;
using EscolaTransparente.Infraestructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Application.Services
{
    public class EscolaAppService : IEscolaAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEscolaService _escolaService;

        public EscolaAppService(IMapper mapper, IUnitOfWork unitOfWork, IEscolaService escolaService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _escolaService = escolaService;
        }

        public async Task<EscolaReadDTO?> AdicionarEscola(EscolaInsertDTO escola)
        {
            try
            {
                var escolaModel = await MapearEscolaValida(escola);

                escolaModel = await PersistirERetornarEscolaCriada(escolaModel);

                return _mapper.Map<EscolaReadDTO>(escolaModel);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar escola: " + ex.Message);
            }
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

        private async Task<EscolaModel> PersistirERetornarEscolaCriada(EscolaModel escolaModel)
        {
            await _unitOfWork.Escolas.AddAsync(escolaModel);
            await _unitOfWork.CommitAsync();

            return escolaModel;
        }

        private async Task<EscolaModel> MapearEscolaValida(EscolaInsertDTO escola)
        {
            var escolaMapeada = _mapper.Map<EscolaModel>(escola);
            return await _escolaService.ValidarEscola(escolaMapeada);
        }

    }
}
