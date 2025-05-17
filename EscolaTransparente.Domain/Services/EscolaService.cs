using EscolaTransparente.Domain.Entities;
using EscolaTransparente.Domain.Interfaces.Services;
using EscolaTransparente.Domain.Validations;
using FluentValidation;

namespace EscolaTransparente.Domain.Services
{
    public class EscolaService : IEscolaService
    {
        private readonly IValidator<EscolaModel> _validator;

        public EscolaService()
        {
            _validator = new EscolaValidation();
        }

        public async Task<bool> ValidarSeUsuarioPodeAlterarDadosEscola(string escolaIdUsuario, int escolaId)
        {
            if(String.IsNullOrEmpty(escolaIdUsuario))
                throw new Exception("O usuário está cadastrado como um gestor escolar, mas não possui nenhuma escola associada ao seu perfil.");

            return int.Parse(escolaIdUsuario) == escolaId;
        }

        public async Task<EscolaModel> ValidarEscola(EscolaModel escola)
        {
            var validationResult = await _validator.ValidateAsync(escola);
            
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }

            return escola;
        }
    }
}
