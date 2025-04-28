using AutoMapper;
using EscolaTransparente.Application.Data.DataTransferObjects.Avaliacao;
using EscolaTransparente.Application.Data.DataTransferObjects.Caracteristica;
using EscolaTransparente.Application.Data.DataTransferObjects.Contato;
using EscolaTransparente.Application.Data.DataTransferObjects.Endereco;
using EscolaTransparente.Application.Data.DataTransferObjects.Escola;
using EscolaTransparente.Application.Data.DataTransferObjects.RespostaAvaliacao;
using EscolaTransparente.Domain.Entities;

namespace EscolaTransparente.Application.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            var caracteristicaMap = CreateMap<CaracteristicasEscolaModel, CaracteristicasEscolaReadDTO>();
            caracteristicaMap.ForMember(dest => dest.Caracteristica, src => src.MapFrom(fld => fld.Caracteristica.Descricao));

            CreateMap<ContatoModel, ContatoReadDTO>().ReverseMap();
            CreateMap<AvaliacaoModel, AvaliacaoReadDTO>().ReverseMap();
            //CreateMap<CaracteristicaModel, CaracteristicaDTO>().ReverseMap();
            CreateMap<CaracteristicasEscolaModel, CaracteristicasEscolaReadDTO>().ReverseMap();
            CreateMap<EnderecoModel, EnderecoReadDTO>().ReverseMap();
            CreateMap<EscolaModel, EscolaReadDTO>().ReverseMap();
            CreateMap<RespostaAvaliacaoModel, RespostaReadAvaliacaoDTO>().ReverseMap(); 
        }
    }
}
