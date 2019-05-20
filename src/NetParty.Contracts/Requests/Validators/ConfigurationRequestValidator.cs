using FluentValidation;

namespace NetParty.Contracts.Requests.Validators
{
    public class ConfigurationRequestValidator : AbstractValidator<ConfigurationRequest>
    {
        public ConfigurationRequestValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
