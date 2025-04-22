using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult ObterEscolaPorId(int escolaId)
        {
            try
            {
                return Ok(_escolaService.ObterEscolaPorId(escolaId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CadastrarEscola")]
        public IActionResult CadastrarEscola(EscolaDTO escolaDTO)
        {
            try
            {
                return Ok(_escolaService.AdicionarEscola(escolaDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
