using EscolaTransparente.API.Attributes;
using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;
using EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica;
using EscolaTransparente.Application.Data.DataTransferObjects.RespostaAvaliacao;
using EscolaTransparente.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaTransparente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoAppService _avaliacaoService;

        public AvaliacaoController(IAvaliacaoAppService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

       
        [HttpPost("")]
        public async Task<ActionResult<List<AvaliacaoReadDTO>>> AvaliarEscola([FromBody] List<AvaliacaoInsertDTO> avaliacoes)
        {
            try
            {
                var result = await _avaliacaoService.AvaliarEscola(avaliacoes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ResponderAvaliacao")]
        public async Task<ActionResult<RespostaReadAvaliacaoDTO>> ResponderAvaliacao([FromBody] RespostaAvaliacaoInsertDTO resposta)
        {
            try
            {
                var result = await _avaliacaoService.ResponderAvaliacao(resposta);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ObterAvaliacoesPorEscolaId/{escolaId:int}")]
        public async Task<ActionResult<List<AvaliacaoPorEscolaRequestDTO>>> ObterAvaliacoesPorEscolaId([FromRoute] int escolaId)
        {
            try
            {
                var result = await _avaliacaoService.ObterAvaliacoesPorEscolaId(escolaId);

                if (result is null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{avaliacaoId:int}")]
        public async Task<ActionResult<AvaliacaoReadDTO>> ObterPorId([FromRoute] int avaliacaoId)
        {
            try
            {
                var result = await _avaliacaoService.ObterAvaliacaoPorId(avaliacaoId);

                if (result is null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{avaliacaoId:int}")]
        public async Task<ActionResult<AvaliacaoReadDTO>> Atualizar([FromRoute] int avaliacaoId, [FromBody] AvaliacaoUpdateDTO avaliacaoDTO)
        {
            try
            {
                var result = await _avaliacaoService.AtualizarAvaliacao(avaliacaoId, avaliacaoDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{avaliacaoId:int}")]
        public async Task<ActionResult> Deletar([FromRoute] int avaliacaoId)
        {
            try
            {
                var result = await _avaliacaoService.DeletarAvaliacao(avaliacaoId);
                if (result)
                    return NoContent();
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObterCaracteristicasPorEscolaId/{escolaId:int}")]
        public async Task<ActionResult<List<CaracteristicaReadDTO>>> ObterCaracteristicasPorEscolaId([FromRoute] int escolaId)
        {
            try
            {
                var result = await _avaliacaoService.ObterCaracteristicasEscolaPorEscolaId(escolaId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Caracteristica")]
        public async Task<ActionResult<CaracteristicaReadDTO>> AdicionarCaracteristica([FromBody] CaracteristicaInsertDTO caracteristica)
        {
            try
            {
                var result = await _avaliacaoService.AdicionarCaracteristica(caracteristica);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CaracteristicaEscola")]
        [HttpPut("{escolaId:int}")]
        [AuthorizeRolesAndClaims(
            roles: new[] { "gestor_escolar" }
        )]
        public async Task<ActionResult<CaracteristicaReadDTO>> AdicionarCaracteristicaEscola([FromBody] CaracteristicaEscolaInsertDTO caracteristicaEscola)
        {
            try
            {
                var result = await _avaliacaoService.AdicionarCaracteristicaEscola(caracteristicaEscola);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
