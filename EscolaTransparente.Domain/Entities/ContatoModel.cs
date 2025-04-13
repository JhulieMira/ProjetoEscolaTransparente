namespace EscolaTransparente.Infraestructure.Data.DataModel
{
    public class ContatoModel
    {
        public int ContatoId { get; set; }
        public int EscolaId { get; set; }
        public string Email { get; set; }
        public string UrlSite { get; set; }
        public string NumeroCelular { get; set; }
        public string NumeroFixo { get; set; }

        public EscolaModel Escola { get; set; }
    }
}
