namespace EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica
{
    public class CaracteristicasEscolaReadDTO
    {
        public int CaracteristicaId { get; set; }
        public int EscolaId { get; set; }
        public string Descricao { get; set; }
        public short NotaMedia { get; set; }
    }
}
