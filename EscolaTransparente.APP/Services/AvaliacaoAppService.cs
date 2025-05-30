using AutoMapper;
using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;
using EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica;
using EscolaTransparente.Application.Interfaces;
using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Domain.Interfaces.Services;
using EscolaTransparente.Infraestructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using EscolaTransparente.Application.Data.DataTransferObjects.RespostaAvaliacao;

namespace EscolaTransparente.Application.Services
{
    public class AvaliacaoAppService : IAvaliacaoAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAvaliacaoService _avaliacaoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AvaliacaoAppService(IMapper mapper, IUnitOfWork unitOfWork, IAvaliacaoService avaliacaoService, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _avaliacaoService = avaliacaoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<AvaliacaoPorEscolaRequestDTO?>> ObterAvaliacoesPorEscolaId(int escolaId)
        {
            try
            {
                var dados = await (from a in _unitOfWork.Avaliacoes
                                   join u in _unitOfWork.Usuario on a.UsuarioId equals u.Id into usuarios
                                   from u in usuarios.DefaultIfEmpty()
                                   join c in _unitOfWork.Caracteristicas on a.CaracteristicaId equals c.CaracteristicaId
                                   join r in _unitOfWork.RespostasAvaliacao on a.AvaliacaoId equals r.AvaliacaoId into respostas
                                   from r in respostas.DefaultIfEmpty()
                                   where a.EscolaId == escolaId
                                   select new
                                   {
                                       NomeUsuario = u != null ? u.UserName : "Usuário removido",
                                       Avaliacao = new AvaliacaoRequestDTO
                                       {
                                           AvaliacaoId = a.AvaliacaoId,
                                           Data = a.Data,
                                           NomeCaracteristica = c.Descricao,
                                           Nota = a.Nota,
                                           ConteudoAvaliacao = a.ConteudoAvaliacao,
                                           AvaliacaoAnonima = a.AvaliacaoAnonima,
                                           RespostaAvaliacao = r != null ? new RespostaReadAvaliacaoDTO
                                           {
                                               RespostaId = r.RespostaId,
                                               AvaliacaoId = a.AvaliacaoId,
                                               UsuarioId = u.UserName,
                                               Resposta = r.Resposta,
                                           } : null
                                       }
                                   }).ToListAsync();

                var resultado = dados
                    .GroupBy(x => x.NomeUsuario)
                    .Select(g => new AvaliacaoPorEscolaRequestDTO
                    {
                        NomeUsuario = g.Any(x => x.Avaliacao.AvaliacaoAnonima) ? "Usuário anônimo" : g.Key,
                        Avaliacoes = g.Select(x => x.Avaliacao).ToList(),
                        AvaliacaoAnonima = g.Any(x => x.Avaliacao.AvaliacaoAnonima)
                    })
                    .ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter avaliação: " + ex.Message);
            }
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

        public async Task<RespostaReadAvaliacaoDTO> ResponderAvaliacao(RespostaAvaliacaoInsertDTO respostaDTO)
        {
            try
            {
                var avaliacao = await _unitOfWork.Avaliacoes
                    .Include(a => a.RespostaAvaliacao)
                    .FirstOrDefaultAsync(a => a.AvaliacaoId == respostaDTO.AvaliacaoId);

                if (avaliacao is null)
                    throw new ValidationException("Avaliação não encontrada");

                if (avaliacao.RespostaAvaliacao != null)
                    throw new ValidationException("Esta avaliação já possui uma resposta");

                var respostaModel = _mapper.Map<RespostaAvaliacaoModel>(respostaDTO);
                respostaModel.UsuarioId = await GetUserId();

                await _unitOfWork.RespostasAvaliacao.AddAsync(respostaModel);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<RespostaReadAvaliacaoDTO>(respostaModel);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao responder avaliação: " + ex.Message);
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
                var result = _mapper.Map<List<AvaliacaoReadDTO>>(avaliacoesModel);

                await AtualizarNotaMediaEscola(avaliacoesDTO.Select(x => x.EscolaId).First());

                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Erro ao salvar avaliações: " + ex.Message, ex);
            }
        }

        public async Task<List<CaracteristicaReadDTO>> ObterCaracteristicasEscolaPorEscolaId(int escolaId)
        {
            try
            {
                var caracteristicas = await _unitOfWork.CaracteristicasEscolas
                    .Include(ce => ce.Caracteristica)
                    .Where(ce => ce.EscolaId == escolaId)
                    .Select(ce => ce.Caracteristica)
                    .ToListAsync();

                if (caracteristicas is null || !caracteristicas.Any())
                    return new List<CaracteristicaReadDTO>();

                return _mapper.Map<List<CaracteristicaReadDTO>>(caracteristicas);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter características da escola: " + ex.Message);
            }
        }

        private async Task<List<AvaliacaoModel>> ProcessarERetornarListaAvaliacoesModel(List<AvaliacaoInsertDTO> avaliacoesDTO)
        {
            List<AvaliacaoModel> avaliacoesModel = new List<AvaliacaoModel>();
            var userId = await GetUserId();

            foreach (var dto in avaliacoesDTO)
            {
                dto.UsuarioId = userId;
                var model = _mapper.Map<AvaliacaoModel>(dto);

                await ResolverDependenciasCaracteristicas(dto, model);
                
                _unitOfWork.Avaliacoes.Add(model);
                avaliacoesModel.Add(model);
            }

            return avaliacoesModel;
        }

        private async Task AtualizarNotaMediaEscola(int escolaId)
        {
            var escola = await _unitOfWork.Escolas.FindAsync(escolaId);

            if (escola is null)
                throw new Exception("Escola não encontrada");

            var mediaNota = await _unitOfWork.Avaliacoes.Where(a => a.EscolaId == escolaId)
                .AverageAsync(a => a.Nota);

            escola.NotaMedia = Convert.ToInt16(mediaNota);

            _unitOfWork.Escolas.Update(escola);
            _unitOfWork.Commit();
        }

        private async Task<string> GetUserId()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _unitOfWork.Usuario.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (string.IsNullOrEmpty(user.Id))
                throw new UnauthorizedAccessException("Usuário não autenticado");

            return user.Id;
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

        public async Task<CaracteristicaReadDTO> AdicionarCaracteristica(CaracteristicaInsertDTO caracteristicaDTO)
        {
            try
            {
                var caracteristica = _mapper.Map<CaracteristicaModel>(caracteristicaDTO);
                
                await _unitOfWork.Caracteristicas.AddAsync(caracteristica);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<CaracteristicaReadDTO>(caracteristica);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar característica: " + ex.Message);
            }
        }

        public async Task<bool> VerificarSeUsuarioRealizouAvaliacao(int escolaId, string userId)
        {
            var avaliacoes = await _unitOfWork.Avaliacoes.Where(x => x.UsuarioId == userId && x.EscolaId == escolaId).ToListAsync();

            return avaliacoes.Any();
        }
        public async Task<CaracteristicaReadDTO> AdicionarCaracteristicaEscola(CaracteristicaEscolaInsertDTO caracteristicaEscolaDTO)
        {
            try
            {
                var escola = await _unitOfWork.Escolas.FindAsync(caracteristicaEscolaDTO.EscolaId);
                if (escola == null)
                    throw new ValidationException("Escola não encontrada");

                CaracteristicaModel caracteristica;

                if (caracteristicaEscolaDTO.CaracteristicaId.HasValue)
                {
                    caracteristica = await _unitOfWork.Caracteristicas.FindAsync(caracteristicaEscolaDTO.CaracteristicaId.Value);
                    if (caracteristica == null)
                        throw new ValidationException("Característica não encontrada");
                }
                else if (!string.IsNullOrEmpty(caracteristicaEscolaDTO.DescricaoCaracteristica))
                {
                    caracteristica = new CaracteristicaModel { Descricao = caracteristicaEscolaDTO.DescricaoCaracteristica };
                    await _unitOfWork.Caracteristicas.AddAsync(caracteristica);
                }
                else
                {
                    throw new ValidationException("Informe CaracteristicaId ou DescricaoCaracteristica");
                }

                var caracteristicaEscola = new CaracteristicasEscolaModel
                {
                    EscolaId = caracteristicaEscolaDTO.EscolaId,
                    CaracteristicaId = caracteristica.CaracteristicaId,
                    Descricao = caracteristica.Descricao,
                    NotaMedia = 0
                };

                await _unitOfWork.CaracteristicasEscolas.AddAsync(caracteristicaEscola);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<CaracteristicaReadDTO>(caracteristica);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar característica à escola: " + ex.Message);
            }
        }
    }
}
