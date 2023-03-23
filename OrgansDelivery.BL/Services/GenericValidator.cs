using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace OrganStorage.BL.Services;

public interface IGenericValidator
{
    Task<ValidationResult> ValidateAsync<T>(T model);
}

public class GenericValidator : IGenericValidator
{
    private readonly IServiceProvider _serviceProvider;

    public GenericValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ValidationResult> ValidateAsync<T>(T model)
    {
        var validator = _serviceProvider.GetService<IValidator<T>>()
            ?? throw new ArgumentException($"Type \"{typeof(T).Name}\" does not have validator");

        return await validator.ValidateAsync(model);
    }
}
