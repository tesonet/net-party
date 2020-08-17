using FluentValidation;

namespace NetParty.Domain.User
{
    public class CredentialsValidator : AbstractValidator<Credentials>
    {
        public CredentialsValidator()
        {
            RuleFor(c => c.UserName)
                .NotNull()
                .NotEmpty()
                .OverridePropertyName("user_name");

            RuleFor(c => c.Password)
                .NotNull()
                .NotEmpty()
                .OverridePropertyName("password");
        }
    }
}