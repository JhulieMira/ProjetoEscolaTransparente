namespace EscolaTransparente.Infraestructure.Data.DataModel
{
    public class AvaliacaoModel
    {
        public int AvaliacaoId { get; set; }
        public int EscolaId { get; set; }
        public string UsuarioId { get; set; }
        public string CaracteristicaId { get; set; }
        public short Nota { get; set; }
        public DateTime Data { get; set; }

        public EscolaModel Escola { get; set; }
        public CaracteristicaModel Caracteristica { get; set; }
        public RespostaAvaliacaoModel RespostaAvaliacao { get; set; }
    }
}
