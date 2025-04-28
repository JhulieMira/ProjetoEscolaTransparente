using System.ComponentModel.DataAnnotations;

namespace EscolaTransparente.Domain.Entities
{
    public class RespostaAvaliacaoModel
    {
        [Key]
        public int RespostaId { get; set; }
        public int AvaliacaoId { get; set; }
        public string UsuarioId { get; set; }
        public string Resposta { get; set; }

        public virtual AvaliacaoModel Avaliacao { get; set; }
    }
}
