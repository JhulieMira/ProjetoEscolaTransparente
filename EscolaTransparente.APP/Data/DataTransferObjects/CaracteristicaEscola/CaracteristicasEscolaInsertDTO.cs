namespace EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica
{
    public class CaracteristicasEscolaInsertDTO
    {
        public int CaracteristicaId { get; set; }
        public int EscolaId { get; set; }
        public string Descricao { get; set; }
    }
}
