using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpGet("ObterEscolaPorId")]
        public async Task<ActionResult<EscolaDTO>> ObterEscolaPorId(int escolaId)
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

        [HttpPost("CadastrarEscola")]
        public async Task<ActionResult<EscolaDTO>> CadastrarEscola(EscolaDTO escolaDTO)
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
