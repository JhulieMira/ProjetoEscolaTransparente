namespace EscolaTransparente.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
