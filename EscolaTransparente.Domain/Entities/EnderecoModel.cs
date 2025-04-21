namespace EscolaTransparente.Domain.Entities
    public class EnderecoModel
    {
        public int EnderecoId { get; set; }
        public int EscolaId { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public EscolaModel Escola { get; set; }
    }
}
