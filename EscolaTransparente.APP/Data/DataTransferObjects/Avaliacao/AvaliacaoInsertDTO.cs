namespace EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao
{
    public class AvaliacaoInsertDTO
    {
        public int EscolaId { get; set; }
        public string UsuarioId { get; set; }
        public int? CaracteristicaId { get; set; }
        public string DescricaoCaracteristica { get; set; }
        public short Nota { get; set; }
        public DateTime Data { get; set; }
        public string? ConteudoAvaliacao { get; set; }
        public bool AvaliacaoAnonima { get; set; }
    }
}
