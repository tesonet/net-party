using System;
using System.Reflection;
using Autofac;
using FluentValidation;

namespace NetParty.Utils
{
    public class FluentValidatorFactory : IValidatorFactory
    {
        private readonly IComponentContext _componentContext;

        public FluentValidatorFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public IValidator<T> GetValidator<T>()
        {
            return (IValidator<T>)GetValidator(typeof(T));
        }

        public IValidator GetValidator(Type type)
        {
            IValidator validator;

            try
            {
                validator = CreateInstance(typeof(IValidator<>).MakeGenericType(type));
            }
            catch
            {
                var baseType = type.GetTypeInfo().BaseType;
                if (baseType == null)
                {
                    throw;
                }

                validator = CreateInstance(typeof(IValidator<>).MakeGenericType(baseType));
            }

            return validator;
        }

        public IValidator CreateInstance(Type validatorType)
        {
            return _componentContext.Resolve(validatorType) as IValidator;
        }
    }
}
