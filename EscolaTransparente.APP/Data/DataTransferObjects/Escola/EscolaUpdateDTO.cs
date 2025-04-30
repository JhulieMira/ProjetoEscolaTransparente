using EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica;
using EscolaTransparente.Application.Data.Enums;

namespace EscolaTransparente.Application.Data.DataTransferObjects.Escola
{
    public class EscolaUpdateDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataCadastro { get; set; }

        public NivelEnsino NivelEnsino { get; set; }
        public TipoInstituicao TipoInstituicao { get; set; }
    }
}
