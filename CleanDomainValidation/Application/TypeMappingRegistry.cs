using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

/// <summary>
/// Registers mappings for inherited properties
/// </summary>
/// <typeparam name="TBasePropertyParameters">Base parameter type</typeparam>
/// <typeparam name="TBaseProperty">Base property type</typeparam>
public sealed class TypeMappingRegistry<TBasePropertyParameters, TBaseProperty>
    where TBasePropertyParameters: class
    where TBaseProperty : class
{
    private readonly Dictionary<Type,Func<TBasePropertyParameters, NameStack, CanFail<TBaseProperty>>> _registeredMappings = [];

    /// <summary>
    /// Register mapping from type <typeparamref name="TDerivedPropertyParameters"/> to <typeparamref name="TDerivedProperty"/> using the property builder <paramref name="propertyBuilder"/>
    /// </summary>
    /// <returns>type mapping instance for chaining registration calls</returns>
    public TypeMappingRegistry<TBasePropertyParameters, TBaseProperty> Register<TDerivedPropertyParameters,
        TDerivedProperty>(
        Func<RequiredPropertyBuilder<TDerivedPropertyParameters, TDerivedProperty>, ValidatedRequiredProperty<TDerivedProperty>> propertyBuilder)
    where TDerivedPropertyParameters : TBasePropertyParameters
    where TDerivedProperty : TBaseProperty
    {
        _registeredMappings[typeof(TDerivedPropertyParameters)] = (builderParameters, nameStack) =>
        {
            var builder = new RequiredPropertyBuilder<TDerivedPropertyParameters, TDerivedProperty>(
                (TDerivedPropertyParameters) builderParameters,
                nameStack);
            
            var buildResult = propertyBuilder(builder).Build();
            return buildResult.HasFailed
                ? CanFail<TBaseProperty>.FromErrors(buildResult.Errors)
                : CanFail<TBaseProperty>.Success(buildResult.Value);
        };

        return this;
    }

    internal CanFail<TBaseProperty> GetResult(TBasePropertyParameters parameters, NameStack nameStack)
    {
        if (!_registeredMappings.TryGetValue(parameters.GetType(), out var factory))
        {
            throw new InvalidOperationException($"No registered mappings found for parameters of type {parameters.GetType()}");
        }
        
        return factory(parameters, nameStack);
    }
}