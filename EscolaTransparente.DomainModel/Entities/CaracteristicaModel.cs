using System.ComponentModel.DataAnnotations;

namespace EscolaTransparente.Domain.Entities
{
    public class CaracteristicaModel
    {
        [Key]
        public int CaracteristicaId { get; set; }
        public string Descricao { get; set; }
    }
}
