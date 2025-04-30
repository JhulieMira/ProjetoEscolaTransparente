using AutoMapper;
using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;
using EscolaTransparente.Application.Interfaces;
using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Domain.Interfaces.Services;
using EscolaTransparente.Infraestructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EscolaTransparente.Application.Services
{
    public class AvaliacaoAppService : IAvaliacaoAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoAppService(IMapper mapper, IUnitOfWork unitOfWork, IAvaliacaoService avaliacaoService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _avaliacaoService = avaliacaoService;
        }

        public async Task<AvaliacaoReadDTO?> ObterAvaliacaoPorId(int avaliacaoId)
        {
            try
            {
                var avaliacao = await _unitOfWork.Avaliacoes
                    .Include(a => a.Escola)
                    .Include(a => a.Caracteristica)
                    .Include(a => a.RespostaAvaliacao)
                    .FirstOrDefaultAsync(a => a.AvaliacaoId == avaliacaoId);

                if (avaliacao is null)
                    return null;

                return _mapper.Map<AvaliacaoReadDTO>(avaliacao);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter avaliação: " + ex.Message);
            }
        }

        public async Task<AvaliacaoReadDTO> AdicionarAvaliacao(AvaliacaoInsertDTO avaliacaoDTO)
        {
            try
            {
                var avaliacaoModel = await MapearEValidarAvaliacaoDTO(avaliacaoDTO);
                avaliacaoModel = await PersistirERetornarAvaliacaoCriada(avaliacaoModel);
                return _mapper.Map<AvaliacaoReadDTO>(avaliacaoModel);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar avaliação: " + ex.Message);
            }
        }

        public async Task<AvaliacaoReadDTO> AtualizarAvaliacao(int avaliacaoId, AvaliacaoUpdateDTO avaliacaoDTO)
        {
            try
            {
                var avaliacaoExistente = await ObterAvaliacaoModelPorId(avaliacaoId);

                if (avaliacaoExistente is null)
                    throw new Exception("Avaliação não encontrada");

                var avaliacaoAtualizada = await MapearEValidarAvaliacaoUpdateDTO(avaliacaoDTO, avaliacaoExistente);

                await _unitOfWork.CommitAsync();

                return _mapper.Map<AvaliacaoReadDTO>(avaliacaoAtualizada);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar avaliação: " + ex.Message);
            }
        }

        public async Task<bool> DeletarAvaliacao(int avaliacaoId)
        {
            try
            {
                var avaliacao = await _unitOfWork.Avaliacoes.FindAsync(avaliacaoId);
                if (avaliacao is null)
                    return false;

                _unitOfWork.Avaliacoes.Remove(avaliacao);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar avaliação: " + ex.Message);
            }
        }

        public async Task<List<AvaliacaoReadDTO>> AvaliarEscola(List<AvaliacaoInsertDTO> avaliacoesDTO)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                await ValidarListaAvaliacoesDTO(avaliacoesDTO);
                var avaliacoesModel = await ProcessarERetornarListaAvaliacoesModel(avaliacoesDTO);

                await _avaliacaoService.ValidarListaAvaliacoes(avaliacoesModel);

                await _unitOfWork.CommitAsync(transaction);
                return _mapper.Map<List<AvaliacaoReadDTO>>(avaliacoesModel);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Erro ao salvar avaliações: " + ex.Message, ex);
            }
        }

        private async Task<List<AvaliacaoModel>> ProcessarERetornarListaAvaliacoesModel(List<AvaliacaoInsertDTO> avaliacoesDTO)
        {
            List<AvaliacaoModel> avaliacoesModel = new List<AvaliacaoModel>();
            foreach (var dto in avaliacoesDTO)
            {
                var model = _mapper.Map<AvaliacaoModel>(dto);

                await ResolverDependenciasCaracteristicas(dto, model);
                
                _unitOfWork.Avaliacoes.Add(model);
                avaliacoesModel.Add(model);
            }

            return avaliacoesModel;
        }

        private async Task ResolverDependenciasCaracteristicas(AvaliacaoInsertDTO dto, AvaliacaoModel model)
        {
            if (dto.CaracteristicaId.HasValue)
                model.Caracteristica = await _unitOfWork.Caracteristicas.FindAsync(dto.CaracteristicaId);
            else
                _unitOfWork.Caracteristicas.Add(model.Caracteristica);
        }

        private async Task ValidarListaAvaliacoesDTO(List<AvaliacaoInsertDTO> avaliacoesDTO)
        {
            if (avaliacoesDTO == null || !avaliacoesDTO.Any())
                throw new ValidationException("A lista de avaliações não pode estar vazia");

            var escolaId = avaliacoesDTO.First().EscolaId;
            if (avaliacoesDTO.Any(a => a.EscolaId != escolaId))
                throw new ValidationException("Todas as avaliações devem ser para a mesma escola");

            var escola = await _unitOfWork.Escolas.FindAsync(escolaId);
            if (escola == null)
                throw new ValidationException("Escola não encontrada");

            foreach (var dto in avaliacoesDTO)
            {
                if (dto.CaracteristicaId.HasValue && string.IsNullOrEmpty(dto.DescricaoCaracteristica))
                {
                    var caracteristicaExistente = await _unitOfWork.Caracteristicas.FindAsync(dto.CaracteristicaId);
                    if (caracteristicaExistente is null)
                        throw new ValidationException($"Característica {dto.CaracteristicaId} não encontrada");
                }
                else if (!dto.CaracteristicaId.HasValue && string.IsNullOrEmpty(dto.DescricaoCaracteristica))
                {
                    throw new ValidationException("Informe CaracteristicaId ou DescricaoCaracteristica");
                }
            }
        }

        private async Task<AvaliacaoModel> PersistirERetornarAvaliacaoCriada(AvaliacaoModel avaliacaoModel)
        {
            await _unitOfWork.Avaliacoes.AddAsync(avaliacaoModel);
            await _unitOfWork.CommitAsync();
            return avaliacaoModel;
        }

        private async Task<AvaliacaoModel> MapearEValidarAvaliacaoDTO(AvaliacaoInsertDTO avaliacao)
        {
            var avaliacaoMapeada = _mapper.Map<AvaliacaoModel>(avaliacao);
            return await _avaliacaoService.ValidarAvaliacao(avaliacaoMapeada);
        }

        private async Task<AvaliacaoModel> ObterAvaliacaoModelPorId(int avaliacaoId)
        {
            return await _unitOfWork.Avaliacoes
                .Include(a => a.Escola)
                .Include(a => a.Caracteristica)
                .Include(a => a.RespostaAvaliacao)
                .FirstOrDefaultAsync(a => a.AvaliacaoId == avaliacaoId);
        }

        private async Task<AvaliacaoModel> MapearEValidarAvaliacaoUpdateDTO(AvaliacaoUpdateDTO avaliacaoDTO, AvaliacaoModel avaliacaoExistente)
        {
            _mapper.Map(avaliacaoDTO, avaliacaoExistente);
            return await _avaliacaoService.ValidarAvaliacao(avaliacaoExistente);
        }
    }
}
