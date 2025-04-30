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
            //var caracteristicaMap = CreateMap<CaracteristicasEscolaModel, CaracteristicasEscolaReadDTO>();
            //caracteristicaMap.ForMember(dest => dest.Caracteristica, src => src.MapFrom(fld => fld.Caracteristica.Descricao));

            // Contato mappings
            CreateMap<ContatoModel, ContatoReadDTO>();
            CreateMap<ContatoInsertDTO, ContatoModel>();
            
            // Avaliacao mappings
            CreateMap<AvaliacaoModel, AvaliacaoReadDTO>();
            CreateMap<AvaliacaoInsertDTO, AvaliacaoModel>()
                    .ForMember(dest => dest.AvaliacaoId, opt => opt.Ignore())
                    .ForMember(dest => dest.Escola, opt => opt.Ignore()) 
                    .ForMember(dest => dest.Caracteristica, opt => opt.MapFrom((src, dest) =>
                    {
                        if (src.CaracteristicaId.HasValue)
                        {
                            return new CaracteristicaModel { CaracteristicaId = src.CaracteristicaId.Value };
                        }
                        else if (!string.IsNullOrEmpty(src.DescricaoCaracteristica))
                        {
                            return new CaracteristicaModel { Descricao = src.DescricaoCaracteristica };
                        }
                        return null;
                    }))
                    .ForMember(dest => dest.RespostaAvaliacao, opt => opt.Ignore());

            // Caracteristica mappings
            CreateMap<CaracteristicaInsertDTO, CaracteristicaModel>();
            CreateMap<CaracteristicasEscolaModel, CaracteristicasEscolaReadDTO>()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Caracteristica.Descricao));

            CreateMap<CaracteristicasEscolaInsertDTO, CaracteristicasEscolaModel>()
                 .ForPath(dest => dest.Caracteristica.CaracteristicaId, opt => opt.MapFrom(src => src.CaracteristicaId))
                 .ForPath(dest => dest.Caracteristica.Descricao, opt => opt.MapFrom(src => src.Descricao));
            // Endereco mappings
            CreateMap<EnderecoModel, EnderecoReadDTO>();
            CreateMap<EnderecoInsertDTO, EnderecoModel>();

            // Escola mappings
            CreateMap<EscolaModel, EscolaReadDTO>();
            CreateMap<EscolaInsertDTO, EscolaModel>();
            CreateMap<EscolaUpdateDTO, EscolaModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                    srcMember != null && !srcMember.Equals(GetDefaultValue(srcMember.GetType()))));

            CreateMap<EscolaUpdateDTO, EscolaModel>()
                .ForMember(dest => dest.EscolaId, opt => opt.Ignore())
                .ForMember(dest => dest.NotaMedia, opt => opt.Ignore())
                .ForMember(dest => dest.Verificada, opt => opt.Ignore())
                .ForMember(dest => dest.CriadaEm, opt => opt.Ignore())
                .ForMember(dest => dest.Contato, opt => opt.Ignore())
                .ForMember(dest => dest.Endereco, opt => opt.Ignore())
                .ForMember(dest => dest.Avaliacoes, opt => opt.Ignore());


            // RespostaAvaliacao mappings
            CreateMap<RespostaAvaliacaoModel, RespostaReadAvaliacaoDTO>();
            CreateMap<RespostaAvaliacaoInsertDTO, RespostaAvaliacaoModel>();
        }
        private object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
