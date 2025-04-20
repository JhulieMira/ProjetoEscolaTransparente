using EscolaTransparente.Application.Data.DataTransferObjects.Escola;

namespace EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica
{
    public class CaracteristicasEscolaDTO
    {
        public int CaracteristicasEscolaId { get; set; }
        public int CaracteristicaId { get; set; }
        public int EscolaId { get; set; }
        public short NotaMedia { get; set; }

        public EscolaDTO Escola { get; set; }
        public CaracteristicaDTO Caracteristica { get; set; }
    }
}
