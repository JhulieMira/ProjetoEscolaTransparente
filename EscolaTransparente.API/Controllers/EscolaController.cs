﻿using System.Data;
using EscolaTransparente.API.Attributes;
using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaTransparente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EscolaController : ControllerBase
    {
        private readonly IEscolaAppService _escolaService;
        public EscolaController(IEscolaAppService escolaService)
        {
            _escolaService = escolaService;
        }

        [HttpGet("ObterEscolaResumida/{escolaId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> ObterPorEscolaResumoPorId([FromRoute] int escolaId)
        {
            try
            {

                var result = await _escolaService.ObterEscolaBasicaPorId(escolaId);
                if (result is null) return NotFound();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{escolaId:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<EscolaDetalhadaReadDTO>> ObterPorId([FromRoute] int escolaId)
        {
            try
            {
                var result = await _escolaService.ObterEscolaPorId(escolaId);

                if (result is null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetTop10")]
        public async Task<ActionResult<EscolaDetalhadaReadDTO>> ObterTop10Escolas()
        {
            try
            {
                var result = await _escolaService.ObterTop10EscolasAsync();

                if (result is null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<EscolaDetalhadaReadDTO>> Cadastrar([FromBody] EscolaInsertDTO escolaDTO)
        {
            try
            {
                var result = await _escolaService.AdicionarEscola(escolaDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{escolaId:int}")]
        [AuthorizeRolesAndClaims(
            roles: new[] { "gestor_escolar" }
        )]
        public async Task<ActionResult<EscolaDetalhadaReadDTO>> Atualizar([FromRoute] int escolaId, [FromBody] EscolaUpdateDTO escolaDTO)
        {
            try
            {
                var result = await _escolaService.AtualizarEscola(escolaId, escolaDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{escolaId:int}")]
        [AuthorizeRolesAndClaims(
            roles: new[] { "admin" }
        )]
        public async Task<ActionResult> Deletar([FromRoute] int escolaId)
        {
            try
            {
                var result = await _escolaService.DeletarEscola(escolaId);
                if (result)
                    return NoContent();
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Buscar")]
        [AllowAnonymous]
        public async Task<ActionResult<List<EscolaDetalhadaReadDTO>>> BuscarEscolas([FromBody] EscolaSearchDTO searchParams)
        {
            try
            {
                var result = await _escolaService.BuscarEscolas(searchParams);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
