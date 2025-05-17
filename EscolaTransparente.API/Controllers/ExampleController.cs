using Microsoft.AspNetCore.Mvc;
using EscolaTransparente.API.Attributes;

namespace EscolaTransparente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        // Exemplo de endpoint que requer apenas autenticação
        [HttpGet("public")]
        public IActionResult PublicEndpoint()
        {
            return Ok("Endpoint público");
        }

        // Exemplo de endpoint que requer role "Admin"
        [AuthorizeRolesAndClaims(roles: new[] { "Admin" })]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("Apenas administradores podem acessar");
        }

        // Exemplo de endpoint que requer role "Teacher" e claim "Permission:EditGrades"
        [AuthorizeRolesAndClaims(
            roles: new[] { "Teacher" },
            claims: new[] { "Permission:EditGrades" }
        )]
        [HttpGet("teacher-grades")]
        public IActionResult TeacherGradesEndpoint()
        {
            return Ok("Professores com permissão para editar notas podem acessar");
        }

        // Exemplo de endpoint que requer role "Student" ou claim "Permission:ViewGrades"
        [AuthorizeRolesAndClaims(
            roles: new[] { "Student" },
            claims: new[] { "Permission:ViewGrades" }
        )]
        [HttpGet("view-grades")]
        public IActionResult ViewGradesEndpoint()
        {
            return Ok("Estudantes ou usuários com permissão para visualizar notas podem acessar");
        }
    }
} 