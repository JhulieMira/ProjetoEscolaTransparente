using System.ComponentModel.DataAnnotations;

namespace EscolaTransparente.Domain.Entities
{
    public class CaracteristicasEscolaModel
    {
        [Key]
        public int CaracteristicasEscolaId { get; set; }
        public int CaracteristicaId { get; set; }
        public int EscolaId { get; set; }
        public short NotaMedia { get; set; }

        public virtual EscolaModel Escola { get; set; }
        public virtual CaracteristicaModel Caracteristica { get; set; }
    }
}
