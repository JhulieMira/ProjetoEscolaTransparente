using System.ComponentModel.DataAnnotations;

namespace EscolaTransparente.Domain.Entities
{
    public class EnderecoModel
    {
        [Key]
        public int EnderecoId { get; set; }
        public int EscolaId { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual EscolaModel Escola { get; set; }
    }
}
