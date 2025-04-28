namespace EscolaTransparente.Application.Data.DataTransferObjects.RespostaAvaliacao
{
    public class RespostaReadAvaliacaoDTO
    {
        public int RespostaId { get; set; }
        public int AvaliacaoId { get; set; }
        public string UsuarioId { get; set; }
        public string Resposta { get; set; }
    }
}
