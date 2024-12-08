using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;

namespace CleanDomainValidation.Application.Extensions;

/// <summary>
/// Extension methods for mapping properties that are complex objects itself
/// </summary>
public static class ComplexMapExtensions
{
    #region Class Property

    /// <summary>
    /// Create the nullable class property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
	/// <param name="property"></param>
	/// <param name="propertyParameters">Paramaters needed to create <typeparamref name="TProperty"/></param>
	/// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty? MapComplex<TParameters, TProperty, TPropertyParameters>(
		this OptionalClassProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<OptionalClassPropertyBuilder<TPropertyParameters, TProperty>, ValidatedOptionalClassProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : class
		where TPropertyParameters : class
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			return null;
		}

		var builder = new OptionalClassPropertyBuilder<TPropertyParameters, TProperty>(builderParameters);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return null;
		}

		return buildResult.Value;
	}

    /// <summary>
    /// Create the nullable class property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Paramaters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty? MapComplex<TParameters, TProperty, TPropertyParameters>(
		this OptionalClassProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<OptionalClassPropertyBuilder<TPropertyParameters, TProperty>, ValidatedOptionalClassProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : class
		where TPropertyParameters : struct
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			return null;
		}

		var builder = new OptionalClassPropertyBuilder<TPropertyParameters, TProperty>(builderParameters.Value);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return null;
		}

		return buildResult.Value;
	}

    /// <summary>
    /// Create the non nullable class property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Paramaters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty MapComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredClassProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<RequiredPropertyBuilder<TPropertyParameters, TProperty>, ValidatedRequiredProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : class
		where TPropertyParameters: class
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return null!;
		}

		var builder = new RequiredPropertyBuilder<TPropertyParameters, TProperty>(builderParameters);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return null!;
		}

		return buildResult.Value;
	}

    /// <summary>
    /// Create the non nullable class property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Paramaters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty MapComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredClassProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<RequiredPropertyBuilder<TPropertyParameters, TProperty>, ValidatedRequiredProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : class
		where TPropertyParameters: struct
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return null!;
		}

		var builder = new RequiredPropertyBuilder<TPropertyParameters, TProperty>(builderParameters.Value);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return null!;
		}

		return buildResult.Value;
	}
    
    /// <summary>
    /// Create the non-nullable class property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Parameters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty MapComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredClassWithDefaultProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<RequiredPropertyBuilder<TPropertyParameters, TProperty>, ValidatedRequiredProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : class
		where TPropertyParameters: class
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			return property.DefaultValue;
		}

		var builder = new RequiredPropertyBuilder<TPropertyParameters, TProperty>(builderParameters);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return null!;
		}

		return buildResult.Value;
	}

    /// <summary>
    /// Create the non-nullable class property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Paramaters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty MapComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredClassWithDefaultProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<RequiredPropertyBuilder<TPropertyParameters, TProperty>, ValidatedRequiredProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : class
		where TPropertyParameters: struct
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			return property.DefaultValue;
		}

		var builder = new RequiredPropertyBuilder<TPropertyParameters, TProperty>(builderParameters.Value);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return null!;
		}

		return buildResult.Value;
	}

    #endregion

    #region Struct Property

    /// <summary>
    /// Create the nullable struct property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Paramaters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty? MapComplex<TParameters, TProperty, TPropertyParameters>(
		this OptionalStructProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<OptionalStructPropertyBuilder<TPropertyParameters, TProperty>, ValidatedOptionalStructProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : struct
		where TPropertyParameters : class
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			return null;
		}

		var builder = new OptionalStructPropertyBuilder<TPropertyParameters, TProperty>(builderParameters);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return null;
		}

		return buildResult.Value;
	}

    /// <summary>
    /// Create the nullable struct <typeparamref name="TProperty"/> property from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Paramaters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty? MapComplex<TParameters, TProperty, TPropertyParameters>(
		this OptionalStructProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<OptionalStructPropertyBuilder<TPropertyParameters, TProperty>, ValidatedOptionalStructProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : struct
		where TPropertyParameters : struct
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			return null;
		}

		var builder = new OptionalStructPropertyBuilder<TPropertyParameters, TProperty>(builderParameters.Value);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return null;
		}

		return buildResult.Value;
	}

    /// <summary>
    /// Create the non nullable struct property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Paramaters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty MapComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredStructProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<RequiredPropertyBuilder<TPropertyParameters, TProperty>, ValidatedRequiredProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : struct
		where TPropertyParameters : class
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default;
		}

		var builder = new RequiredPropertyBuilder<TPropertyParameters, TProperty>(builderParameters);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return default;
		}

		return buildResult.Value;
	}

    /// <summary>
    /// Create the non nullable struct property <typeparamref name="TProperty"/> from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">Paramaters needed to create <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static TProperty MapComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredStructProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<RequiredPropertyBuilder<TPropertyParameters, TProperty>, ValidatedRequiredProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TProperty : struct
		where TPropertyParameters : struct
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default;
		}

		var builder = new RequiredPropertyBuilder<TPropertyParameters, TProperty>(builderParameters.Value);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return default;
		}

		return buildResult.Value;
	}

    #endregion

    #region List Property

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/> of the nullable list property from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="propertyParameters">List of Paramaters needed to create List of <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static IEnumerable<TProperty>? MapEachComplex<TParameters, TProperty, TPropertyParameters>(
		this OptionalListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<TPropertyParameters>?> propertyParameters,
		Func<RequiredPropertyBuilder<TPropertyParameters, TProperty>, ValidatedRequiredProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TPropertyParameters : notnull
		where TProperty : notnull
	{
		IEnumerable<TPropertyParameters>? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			return null;
		}

		List<TProperty> resultProperties = [];
		foreach(var rawProperty in builderParameters)
		{
			var builder = new RequiredPropertyBuilder<TPropertyParameters, TProperty>(rawProperty);
			var buildResult = propertyBuilder.Invoke(builder).Build();
			if (buildResult.HasFailed)
			{
				property.ValidationResult.InheritFailure(buildResult);
				continue;
			}
			resultProperties.Add(buildResult.Value);
		}
		return resultProperties;
	}

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/> of the non nullable list property from the parameters specified in <paramref name="propertyParameters"/> by using a <paramref name="propertyBuilder"/>
    /// </summary>
	/// <param name="property"></param>
    /// <param name="propertyParameters">List of Paramaters needed to create List of <typeparamref name="TProperty"/></param>
    /// <param name="propertyBuilder">Builder that creates <typeparamref name="TProperty"/> from <typeparamref name="TPropertyParameters"/></param>
    public static IEnumerable<TProperty> MapEachComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<TPropertyParameters>?> propertyParameters,
		Func<RequiredPropertyBuilder<TPropertyParameters, TProperty>, ValidatedRequiredProperty<TProperty>> propertyBuilder)
		where TParameters : notnull
		where TPropertyParameters : notnull
		where TProperty : notnull
	{
		IEnumerable<TPropertyParameters>? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return null!;
		}

		List<TProperty> resultProperties = [];
		foreach (var rawProperty in builderParameters)
		{
			var builder = new RequiredPropertyBuilder<TPropertyParameters, TProperty>(rawProperty);
			var buildResult = propertyBuilder.Invoke(builder).Build();
			if (buildResult.HasFailed)
			{
				property.ValidationResult.InheritFailure(buildResult);
				continue;
			}
			resultProperties.Add(buildResult.Value);
		}
		return resultProperties;
	}

	#endregion
}
