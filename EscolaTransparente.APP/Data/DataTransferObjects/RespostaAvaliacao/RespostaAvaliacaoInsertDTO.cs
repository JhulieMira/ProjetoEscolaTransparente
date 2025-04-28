namespace EscolaTransparente.Application.Data.DataTransferObjects.RespostaAvaliacao
{
    internal class RespostaAvaliacaoInsertDTO
    {
        public int AvaliacaoId { get; set; }
        public string UsuarioId { get; set; }
        public string Resposta { get; set; }
    }
}
