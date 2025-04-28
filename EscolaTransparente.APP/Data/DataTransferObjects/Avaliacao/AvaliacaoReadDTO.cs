namespace EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao
{
    public class AvaliacaoReadDTO
    {
        public int AvaliacaoId { get; set; }
        public int EscolaId { get; set; }
        public string UsuarioId { get; set; }
        public int CaracteristicaId { get; set; }
        public int Descricao { get; set; }
        public short Nota { get; set; }
        public DateTime Data { get; set; }
    }
}
