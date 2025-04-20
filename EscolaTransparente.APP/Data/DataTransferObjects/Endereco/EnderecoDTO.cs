using EscolaTransparente.Application.Data.DataTransferObjects.Escola;

namespace EscolaTransparente.Application.Data.DataTransferObjects.Endereco
{
    public class EnderecoDTO
    {
        public int EnderecoId { get; set; }
        public int EscolaId { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public EscolaDTO Escola { get; set; }
    }
}
