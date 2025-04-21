namespace EscolaTransparente.Domain.Entities
{
    public class CaracteristicasEscolaModel
    {
        public int CaracteristicasEscolaId { get; set; }
        public int CaracteristicaId { get; set; }
        public int EscolaId { get; set; }
        public short NotaMedia { get; set; }

        public EscolaModel Escola { get; set; }
        public CaracteristicaModel Caracteristica { get; set; }
    }
}
