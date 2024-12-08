using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;

namespace CleanDomainValidation.Application.Extensions;

/// <summary>
/// Extension methods for mapping properties that are the same type as in the parameter
/// </summary>
public static class DirectMapExtensions
{
    #region Class Property

    /// <summary>
    /// Create the nullable class property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is mapped to the property of type <typeparamref name="TProperty"/></param>
    public static TProperty? Map<TParameters, TProperty>(
		this OptionalClassProperty<TParameters, TProperty> property,
		Func<TParameters, TProperty?> value)
		where TParameters : notnull
		where TProperty : class
	{
		TProperty? rawValue = value.Invoke(property.Parameters);
		return rawValue ?? property.DefaultValue;
	}

    /// <summary>
    /// Create the non nullable class property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is mapped to the property of type <typeparamref name="TProperty"/></param>
    public static TProperty Map<TParameters, TProperty>(
		this RequiredClassProperty<TParameters, TProperty> property,
		Func<TParameters, TProperty?> value)
		where TParameters : notnull
		where TProperty : class
	{
		TProperty? rawValue = value.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return null!;
		}

		return rawValue;
	}

    #endregion

    #region Struct Property

    /// <summary>
    /// Create the nullable struct property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is mapped to the property of type <typeparamref name="TProperty"/></param>
    public static TProperty? Map<TParameters, TProperty>(
		this OptionalStructProperty<TParameters, TProperty> property,
		Func<TParameters, TProperty?> value)
		where TParameters : notnull
		where TProperty : struct
	{
		TProperty? rawValue = value.Invoke(property.Parameters);
		return rawValue ?? property.DefaultValue;
	}

    /// <summary>
    /// Create the non nullable struct property <typeparamref name="TProperty"/> from the value specified in <paramref name="value"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="value">Parameter that is mapped to the property of type <typeparamref name="TProperty"/></param>
    public static TProperty Map<TParameters, TProperty>(
		this RequiredStructProperty<TParameters, TProperty> property,
		Func<TParameters, TProperty?> value)
		where TParameters : notnull
		where TProperty : struct
	{
		TProperty? rawValue = value.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default;
		}

		return rawValue.Value;
	}

    #endregion

    #region List Property

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/> of the nullable list property from the values specified in <paramref name="values"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="values">List of parameter that is mapped to the property of type <typeparamref name="TProperty"/></param>
    public static IEnumerable<TProperty>? MapEach<TParameters, TProperty>(
		this OptionalListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<TProperty>?> values)
		where TParameters : notnull
		where TProperty : notnull
	{
		IEnumerable<TProperty>? rawValue = values.Invoke(property.Parameters);

		return rawValue;
	}

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/> of the non nullable list property from the values specified in <paramref name="values"/>
    /// </summary>
    /// <remarks>
    /// If more then one parameter is needed to create an instance of <typeparamref name="TProperty"/>, use the methods provided by <see cref="ComplexMapExtensions"/> instead.
    /// </remarks>
    /// <param name="property"></param>
    /// <param name="values">List of parameter that is mapped to the property of type <typeparamref name="TProperty"/></param>
    public static IEnumerable<TProperty> MapEach<TParameters, TProperty>(
		this RequiredListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<TProperty>?> values)
		where TParameters : notnull
		where TProperty : notnull
	{
		IEnumerable<TProperty>? rawValue = values.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return null!;
		}

		return rawValue;
	}

	#endregion
}
