namespace EscolaTransparente.Infraestructure.Data.DataModel
{
    public class RespostaAvaliacaoModel
    {
        public int RespostaId { get; set; }
        public int AvaliacaoId { get; set; }
        public string UsuarioId { get; set; }
        public string Resposta { get; set; }

        public AvaliacaoModel Avaliacao { get; set; }
    }
}
