namespace EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao
{
    public class AvaliacaoUpdateDTO
    {
        public int Descricao { get; set; }
        public short Nota { get; set; }
        public string UsuarioId { get; set; }
        public DateTime Data { get; set; }
    }
}
