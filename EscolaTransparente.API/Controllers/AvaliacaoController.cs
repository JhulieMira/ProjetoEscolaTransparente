using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;
using EscolaTransparente.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscolaTransparente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
