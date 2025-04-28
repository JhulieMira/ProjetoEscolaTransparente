namespace EscolaTransparente.Domain.Entities
{ 
    public class AvaliacaoModel
    {
        public int AvaliacaoId { get; set; }
        public int EscolaId { get; set; }
        public string UsuarioId { get; set; }
        public int CaracteristicaId { get; set; }
        public short Nota { get; set; }
        public DateTime Data { get; set; }

        public virtual EscolaModel Escola { get; set; }
        public virtual CaracteristicaModel Caracteristica { get; set; }
        public virtual RespostaAvaliacaoModel RespostaAvaliacao { get; set; }
    }
}
