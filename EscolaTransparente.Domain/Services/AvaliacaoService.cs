using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Domain.Interfaces.Services;
using EscolaTransparente.Domain.Validations;
using FluentValidation;

namespace EscolaTransparente.Domain.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IValidator<AvaliacaoModel> _validator;

        public AvaliacaoService()
        {
            _validator = new AvaliacaoValidation();
        }

        public async Task<AvaliacaoModel> ValidarAvaliacao(AvaliacaoModel avaliacao)
        {
            var validationResult = await _validator.ValidateAsync(avaliacao);
            
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            return avaliacao;
        }
    }
} 