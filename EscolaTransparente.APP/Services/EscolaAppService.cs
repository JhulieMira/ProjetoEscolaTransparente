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

        public async Task<EscolaDetalhadaReadDTO?> AdicionarEscola(EscolaInsertDTO escola)
        {
            try
            {
                var escolaModel = await MapearEValidarEscolaDTO(escola);

                escolaModel = await PersistirERetornarEscolaCriada(escolaModel);

                return _mapper.Map<EscolaDetalhadaReadDTO>(escolaModel);
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

        public async Task<EscolaDetalhadaReadDTO?> ObterEscolaPorId(int escolaId)
        {
            try
            {
                var escola = await _unitOfWork.Escolas
                .Include(e => e.Contato)
                .Include(e => e.Endereco)
                .Include(e => e.CaracteristicasEscola)
                    .ThenInclude(ce => ce.Caracteristica)
                .FirstOrDefaultAsync(e => e.EscolaId == escolaId);

                if (escola is null)
                    return null;

                return _mapper.Map<EscolaDetalhadaReadDTO>(escola);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter escola: " + ex.Message);
            }
        }

        public async Task<EscolaDetalhadaReadDTO> AtualizarEscola(int escolaId, EscolaUpdateDTO escolaDTO)
        {
            try
            {
                var escolaExistente = await ObterEscolaModelPorId(escolaId);

                if (escolaExistente is null)
                    throw new Exception("Escola não encontrada");

                var escolaAtualizada = await MapearEValidarEscolaReadDTO(escolaDTO, escolaExistente);

                await _unitOfWork.CommitAsync();

                return _mapper.Map<EscolaDetalhadaReadDTO>(escolaAtualizada);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar escola: " + ex.Message);
            }
        }

        public async Task<bool> DeletarEscola(int escolaId)
        {
            try
            {
                var escola = await _unitOfWork.Escolas.FindAsync(escolaId);
                if (escola is null)
                    return false;

                _unitOfWork.Escolas.Remove(escola);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar escola: " + ex.Message);
            }
        }


        private async Task<EscolaModel> PersistirERetornarEscolaCriada(EscolaModel escolaModel)
        {
            await _unitOfWork.Escolas.AddAsync(escolaModel);
            await _unitOfWork.CommitAsync();

            return escolaModel;
        }

        private async Task<EscolaModel> MapearEValidarEscolaDTO(EscolaInsertDTO escola)
        {
            var escolaMapeada = _mapper.Map<EscolaModel>(escola);
            return await _escolaService.ValidarEscola(escolaMapeada);
        }

        private async Task<EscolaModel> ObterEscolaModelPorId(int escolaId)
        {
            return await _unitOfWork.Escolas
            .Include(e => e.Contato)
            .Include(e => e.Endereco)
            .Include(e => e.CaracteristicasEscola)
                .ThenInclude(ce => ce.Caracteristica)
            .FirstOrDefaultAsync(e => e.EscolaId == escolaId);
        }
        private async Task<EscolaModel> MapearEValidarEscolaReadDTO(EscolaUpdateDTO escolaDTO, EscolaModel escolaExistente)
        {
            _mapper.Map(escolaDTO, escolaExistente);

            return await _escolaService.ValidarEscola(escolaExistente);
        }
    }
}
