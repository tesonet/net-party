using FluentValidation.Results;

namespace NetParty.Interfaces.Services
{
    public interface IValidationService
    {
        ValidationResult Validate<T>(T entity) where T : class;
    }
}
