namespace EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica
{
    public class CaracteristicaEscolaInsertDTO
    {
        public int EscolaId { get; set; }
        public int? CaracteristicaId { get; set; }
        public string DescricaoCaracteristica { get; set; }
    }
} 