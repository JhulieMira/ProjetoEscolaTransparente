using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscolaTransparente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EscolaController : ControllerBase
    {
        private readonly IEscolaAppService _escolaService;
        public EscolaController(IEscolaAppService escolaService)
        {
            _escolaService = escolaService;
        }

        [HttpGet("{escolaId:int}")]
        public async Task<ActionResult<EscolaReadDTO>> ObterPorId([FromRoute]int escolaId)
        {
            try
            {
                var result = await _escolaService.ObterEscolaPorId(escolaId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<EscolaInsertDTO>> Cadastrar([FromBody]EscolaInsertDTO escolaDTO)
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
    }
}
