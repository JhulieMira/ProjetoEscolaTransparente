using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EscolaTransparente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EscolaController : ControllerBase  
    {
        [HttpGet("ObterEscolaPorId")]
        public IActionResult ObterEscolaPorId(int escolaId)
        {
            try
            {
                return Ok();
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
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
