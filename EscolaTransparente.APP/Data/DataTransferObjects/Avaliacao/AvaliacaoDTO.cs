namespace EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao
{
    public class AvaliacaoDTO
    {
        public int AvaliacaoId { get; set; }
        public int EscolaId { get; set; }
        public string UsuarioId { get; set; }
        public int CaracteristicaId { get; set; }
        public short Nota { get; set; }
        public DateTime Data { get; set; }
    }
}
