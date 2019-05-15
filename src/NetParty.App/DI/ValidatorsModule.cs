using Autofac;
using FluentValidation;
using NetParty.Contracts;
using NetParty.Contracts.Validators;
using NetParty.Utils;

namespace NetParty.App.DI
{
    public class ValidatorsModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CredentialsValidator>().As<IValidator<Credentials>>();
            builder.RegisterType<FluentValidatorFactory>().As<IValidatorFactory>();
        }
    }
}
