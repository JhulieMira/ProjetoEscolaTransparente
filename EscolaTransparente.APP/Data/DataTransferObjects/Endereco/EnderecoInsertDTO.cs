namespace EscolaTransparente.Application.Data.DataTransferObjects.Endereco
{
    public class EnderecoInsertDTO
    {
        public int EscolaId { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
