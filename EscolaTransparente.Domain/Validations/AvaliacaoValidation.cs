using FluentValidation;
using EscolaTransparente.Domain.Entities;

namespace EscolaTransparente.Domain.Validations
{
    public class AvaliacaoValidation : AbstractValidator<AvaliacaoModel>
    {
        public AvaliacaoValidation()
        {
            RuleFor(x => x.EscolaId)
                .NotEmpty().WithMessage("O ID da escola é obrigatório")
                .GreaterThan(0).WithMessage("O ID da escola deve ser maior que zero");

            RuleFor(x => x.UsuarioId)
                .NotEmpty().WithMessage("O ID do usuário é obrigatório");

            RuleFor(x => x.CaracteristicaId)
                .NotEmpty().WithMessage("O ID da característica é obrigatório")
                .GreaterThan(0).WithMessage("O ID da característica deve ser maior que zero");

            RuleFor(x => x.Nota)
                .NotEmpty().WithMessage("A nota é obrigatória")
                .InclusiveBetween((short)1, (short)5).WithMessage("A nota deve estar entre 1 e 5");
        }
    }
} 