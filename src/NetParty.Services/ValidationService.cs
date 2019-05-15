using FluentValidation;
using FluentValidation.Results;
using NetParty.Interfaces.Services;

namespace NetParty.Services
{
    public class FluentValidationService : IValidationService
    {
        private readonly IValidatorFactory _validatorFactory;

        public FluentValidationService(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public ValidationResult Validate<T>(T entity) where T : class
        {
            var validator = _validatorFactory.GetValidator(entity.GetType());
            var result = validator.Validate(entity);
            return result;
        }
    }
}
