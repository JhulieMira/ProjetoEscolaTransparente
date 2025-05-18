using AutoMapper;
using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Interfaces;
using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Domain.Interfaces.Services;
using EscolaTransparente.Infraestructure.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using EscolaTransparente.Application.Data.Enums;

namespace EscolaTransparente.Application.Services
{
    public class EscolaAppService : IEscolaAppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEscolaService _escolaService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EscolaAppService(IMapper mapper, IUnitOfWork unitOfWork, IEscolaService escolaService, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _escolaService = escolaService;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<List<EscolaDetalhadaReadDTO>> ObterTop10EscolasAsync()
        {
            try
            {
                var escolas = await _unitOfWork.Escolas
                    .Include(e => e.Contato)
                    .Include(e => e.Endereco)
                    .Include(e => e.CaracteristicasEscola)
                        .ThenInclude(ce => ce.Caracteristica)
                    .Include(e => e.Avaliacoes)
                    .OrderByDescending(e => e.Avaliacoes.Any() ? e.Avaliacoes.Average(a => a.Nota) : 0)
                    .Take(10)
                    .ToListAsync();

                var escolasDTO = escolas.Select(e =>
                {
                    var dto = _mapper.Map<EscolaDetalhadaReadDTO>(e);
                    dto.NotaMedia = (short)(e.Avaliacoes.Any() ? e.Avaliacoes.Average(a => a.Nota) : 0);
                    return dto;
                }).ToList();

                return escolasDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter top 10 escolas: " + ex.Message);
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
                .Include(ce => ce.Avaliacoes)
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

        public async Task<EscolaReadDTO?> ObterEscolaBasicaPorId(int escolaId)
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

                return _mapper.Map<EscolaReadDTO>(escola);
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
                await ValidarPermissaoUsuario(escolaId);

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

        public async Task<List<EscolaDetalhadaReadDTO>> BuscarEscolas(EscolaSearchDTO searchParams)
        {
            try
            {
                var query = _unitOfWork.Escolas
                    .Include(e => e.Contato)
                    .Include(e => e.Endereco)
                    .Include(e => e.CaracteristicasEscola)
                        .ThenInclude(ce => ce.Caracteristica)
                    .Include(e => e.Avaliacoes)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchParams.NomeEscola))
                {
                    query = query.Where(e => e.Nome.Contains(searchParams.NomeEscola));
                }

                if (!String.IsNullOrEmpty(searchParams.NivelEnsino))
                {
                    if (Enum.TryParse(searchParams.NivelEnsino, ignoreCase: true, out NivelEnsino nivelEnsinoEnum));
                }

                if (!string.IsNullOrWhiteSpace(searchParams.CEP))
                {
                    query = query.Where(e => e.Endereco.CEP == searchParams.CEP);
                }


                var escolas = await query.ToListAsync();

                var escolasDTO = escolas.Select(e =>
                {   
                    var dto = _mapper.Map<EscolaDetalhadaReadDTO>(e);
                    dto.NotaMedia = (short)(e.Avaliacoes.Any() ? e.Avaliacoes.Average(a => a.Nota) : 0);
                    return dto;
                }).ToList();

                return escolasDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar escolas: " + ex.Message);
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
        private async Task ValidarPermissaoUsuario(int escolaId)
        {
            var escolaIdUsuarioAutenticado = GetUserClaim("escolaId");

            var usuarioPossuiPermissao = await _escolaService.ValidarSeUsuarioPodeAlterarDadosEscola(escolaIdUsuarioAutenticado, escolaId);

            if (!usuarioPossuiPermissao)
                throw new Exception("Usuário não possui permissão para alterar os dados da escola.");
        }

        private string GetUserClaim(string claimType)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
                return null;

            var claim = user.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim?.Value;
        }
    }
}
