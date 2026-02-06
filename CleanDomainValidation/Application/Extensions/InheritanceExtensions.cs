using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Extensions;

/// <summary>
/// Extension methods for mapping properties that inherit from another
/// </summary>
public static class InheritanceExtensions
{
    /// <summary>
    /// Create the nullable class property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using the mapping registry configured in <paramref name="configureRegistry"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Parameters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="configureRegistry">Registry that holds all the type mappings</param>
    public static TProperty? MapInherited<TParameters, TProperty, TPropertyParameters>(
        this OptionalClassProperty<TParameters, TProperty> property,
        Func<TParameters, TPropertyParameters?> propertyParameters,
        Action<TypeMappingRegistry<TPropertyParameters, TProperty>> configureRegistry)
        where TParameters : notnull
        where TProperty : class
        where TPropertyParameters: class
    {
        TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
        if (builderParameters is null)
        {
            property.NameStack.Pop();
            return null;
        }
        
        var registry = new TypeMappingRegistry<TPropertyParameters, TProperty>();
        configureRegistry(registry);

        var buildResult = registry.GetResult(builderParameters, property.NameStack);
		
        property.NameStack.Pop();
		
        if (buildResult.HasFailed)
        {
            property.ValidationResult.InheritFailure(buildResult);
            return null!;
        }
		
        return buildResult.Value;
    }
    
    /// <summary>
    /// Create the non-nullable class property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using the mapping registry configured in <paramref name="configureRegistry"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Parameters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="configureRegistry">Registry that holds all the type mappings</param>
    public static TProperty MapInherited<TParameters, TProperty, TPropertyParameters>(
        this RequiredClassProperty<TParameters, TProperty> property,
        Func<TParameters, TPropertyParameters?> propertyParameters,
        Action<TypeMappingRegistry<TPropertyParameters, TProperty>> configureRegistry)
        where TParameters : notnull
        where TProperty : class
        where TPropertyParameters: class
    {
        TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
        if (builderParameters is null)
        {
            property.ValidationResult.Failed(property.MissingError);
            property.NameStack.Pop();
            return null!;
        }
        
        var registry = new TypeMappingRegistry<TPropertyParameters, TProperty>();
        configureRegistry(registry);

        var buildResult = registry.GetResult(builderParameters, property.NameStack);
		
        property.NameStack.Pop();
		
        if (buildResult.HasFailed)
        {
            property.ValidationResult.InheritFailure(buildResult);
            return null!;
        }
		
        return buildResult.Value;
    }
}