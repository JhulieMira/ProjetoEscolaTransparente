using EscolaTransparente.Application.Data.Enums;

namespace EscolaTransparente.Application.Data.DataTransferObjects.Escola
{
    public class EscolaSearchDTO
    {
        public string? NomeEscola { get; set; }
        public int? NivelEnsino { get; set; }
        public string? CEP { get; set; }
    }
} 