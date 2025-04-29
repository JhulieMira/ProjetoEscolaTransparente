using System.ComponentModel.DataAnnotations;

namespace EscolaTransparente.Domain.Entities
{
    public class ContatoModel
    {
        [Key]
        public int ContatoId { get; set; }
        public int EscolaId { get; set; }
        public string Email { get; set; }
        public string UrlSite { get; set; }
        public string NumeroCelular { get; set; }
        public string NumeroFixo { get; set; }

        public virtual EscolaModel Escola { get; set; }
    }
}
