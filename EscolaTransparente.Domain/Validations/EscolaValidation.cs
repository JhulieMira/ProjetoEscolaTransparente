using FluentValidation;
using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Infraestructure.Data.Enums;

namespace EscolaTransparente.Domain.Validations
{
    public class EscolaValidation : AbstractValidator<EscolaModel>
    {
        public EscolaValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da escola é obrigatório")
                .MaximumLength(200).WithMessage("O nome da escola não pode ter mais que 200 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição da escola é obrigatória")
                .MaximumLength(1000).WithMessage("A descrição não pode ter mais que 1000 caracteres");

            RuleFor(x => x.CNPJ)
                .NotEmpty().WithMessage("O CNPJ é obrigatório para escolas privadas")
                    .When(x => x.TipoInstituicao == TipoInstituicao.Privada)
                .Length(14).WithMessage("O CNPJ deve ter 14 dígitos")
                    .When(x => x.TipoInstituicao == TipoInstituicao.Privada)
                .Matches(@"^\d+$").WithMessage("O CNPJ deve conter apenas números")
                    .When(x => x.TipoInstituicao == TipoInstituicao.Privada);

            RuleFor(x => x.Contato)
                .NotNull().WithMessage("As informações de contato são obrigatórias");

            RuleFor(x => x.Endereco)
                .NotNull().WithMessage("O endereço é obrigatório");
        }
    }
} 