using EscolaTransparente.Application.Data.DataTransferObjects.Escola;

namespace EscolaTransparente.Application.Data.DataTransferObjects.Contato
{
    public class ContatoReadDTO
    {
        public int ContatoId { get; set; }
        public int EscolaId { get; set; }
        public string Email { get; set; }
        public string UrlSite { get; set; }
        public string NumeroCelular { get; set; }
        public string NumeroFixo { get; set; }
    }
}
