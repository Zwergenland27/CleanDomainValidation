using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;

namespace CleanDomainValidation.Application.Extensions;

/// <summary>
/// Extension methods for mapping properties that can be created by using a constructor
/// </summary>
public static class ConstructorMapExtensions
{
    #region Class Property

    /// <summary>
    /// Create the nullable class property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static TProperty? Map<TParameters, TProperty, TValue>(
        this OptionalClassProperty<TParameters, TProperty> property,
        Func<TParameters, TValue?> value,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : class
        where TValue : class
    {
        TValue? rawValue = value.Invoke(property.Parameters);

        if (rawValue is null)
        {
            return property.DefaultValue;
        }

        return constructor.Invoke(rawValue);
    }

    /// <summary>
    /// Create the nullable class property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static TProperty? Map<TParameters, TProperty, TValue>(
        this OptionalClassProperty<TParameters, TProperty> property,
        Func<TParameters, TValue?> value,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : class
        where TValue : struct
    {
        TValue? rawValue = value.Invoke(property.Parameters);

        if (rawValue is null)
        {
            return property.DefaultValue;
        }

        return constructor.Invoke(rawValue.Value);
    }

    /// <summary>
    /// Create the non nullable class property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static TProperty Map<TParameters, TProperty, TValue>(
        this RequiredClassProperty<TParameters, TProperty> property,
        Func<TParameters, TValue?> value,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : class
        where TValue : class
    {
        TValue? rawValue = value.Invoke(property.Parameters);
        if (rawValue is null)
        {
            property.ValidationResult.Failed(property.MissingError);
            return null!;
        }

        return constructor.Invoke(rawValue);
    }

    /// <summary>
    /// Create the non nullable class property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static TProperty Map<TParameters, TProperty, TValue>(
        this RequiredClassProperty<TParameters, TProperty> property,
        Func<TParameters, TValue?> value,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : class
        where TValue : struct
    {
        TValue? rawValue = value.Invoke(property.Parameters);
        if (rawValue is null)
        {
            property.ValidationResult.Failed(property.MissingError);
            return null!;
        }

        return constructor.Invoke(rawValue.Value);
    }

    #endregion

    #region Struct Property

    /// <summary>
    /// Create the nullable struct property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static TProperty? Map<TParameters, TProperty, TValue>(
        this OptionalStructProperty<TParameters, TProperty> property,
        Func<TParameters, TValue?> value,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : struct
        where TValue : class
    {
        TValue? rawValue = value.Invoke(property.Parameters);

        if (rawValue is null)
        {
            return null;
        }

        return constructor.Invoke(rawValue);
    }

    /// <summary>
    /// Create the nullable struct property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static TProperty? Map<TParameters, TProperty, TValue>(
        this OptionalStructProperty<TParameters, TProperty> property,
        Func<TParameters, TValue?> value,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : struct
        where TValue : struct
    {
        TValue? rawValue = value.Invoke(property.Parameters);

        if (rawValue is null)
        {
            return null;
        }

        return constructor.Invoke(rawValue.Value);
    }

    /// <summary>
    /// Create the non nullable struct property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static TProperty Map<TParameters, TProperty, TValue>(
        this RequiredStructProperty<TParameters, TProperty> property,
        Func<TParameters, TValue?> value,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : struct
        where TValue : class
    {
        TValue? rawValue = value.Invoke(property.Parameters);
        if (rawValue is null)
        {
            property.ValidationResult.Failed(property.MissingError);
            return default;
        }

        return constructor.Invoke(rawValue);
    }

    /// <summary>
    /// Create the non nullable struct property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static TProperty Map<TParameters, TProperty, TValue>(
        this RequiredStructProperty<TParameters, TProperty> property,
        Func<TParameters, TValue?> value,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : struct
        where TValue : struct
    {
        TValue? rawValue = value.Invoke(property.Parameters);
        if (rawValue is null)
        {
            property.ValidationResult.Failed(property.MissingError);
            return default;
        }

        return constructor.Invoke(rawValue.Value);
    }

    #endregion

    #region List Property

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/> of the nullable list property from the values specified in <paramref name="values"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="values">List of parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static IEnumerable<TProperty>? MapEach<TParameters, TProperty, TValue>(
        this OptionalListProperty<TParameters, TProperty> property,
        Func<TParameters, IEnumerable<TValue>?> values,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : notnull
    {
        IEnumerable<TValue>? rawValues = values.Invoke(property.Parameters);
        if (rawValues is null)
        {
            return null;
        }

        List<TProperty> resultProperties = [];
        foreach (var rawProperty in rawValues)
        {
            TProperty result = constructor.Invoke(rawProperty);

            resultProperties.Add(result);
        }

        return resultProperties;
    }

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/> of the non nullable list property from the values specified in <paramref name="values"/> and passing the constructor to <paramref name="constructor"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// If you want to use a factory method to create the property, use the methods provided by <see cref="FactoryMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="values">List of parameter that is needed for the constructor of <typeparamref name="TProperty"/></param>
    /// <param name="constructor">Lambda function that calls the constructor of <typeparamref name="TProperty"/></param>
    public static IEnumerable<TProperty> MapEach<TParameters, TProperty, TValue>(
        this RequiredListProperty<TParameters, TProperty> property,
        Func<TParameters, IEnumerable<TValue>?> values,
        Func<TValue, TProperty> constructor)
        where TParameters : notnull
        where TProperty : notnull
    {
        IEnumerable<TValue>? rawValues = values.Invoke(property.Parameters);
        if (rawValues is null)
        {
            property.ValidationResult.Failed(property.MissingError);
            return null!;
        }

        List<TProperty> resultProperties = [];
        foreach (var rawProperty in rawValues)
        {
            TProperty result = constructor.Invoke(rawProperty);

            resultProperties.Add(result);
        }

        return resultProperties;
    }

    #endregion
}
