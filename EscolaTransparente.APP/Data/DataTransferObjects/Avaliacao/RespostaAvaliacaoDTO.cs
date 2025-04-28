namespace EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao
{
    public class RespostaAvaliacaoDTO
    {
        public int RespostaId { get; set; }
        public int AvaliacaoId { get; set; }
        public string UsuarioId { get; set; }
        public string Resposta { get; set; }
    }
}
