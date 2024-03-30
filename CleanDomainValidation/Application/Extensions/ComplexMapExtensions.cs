using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;

namespace CleanDomainValidation.Application.Extensions;

public static class ComplexMapExtensions
{
	#region Class Property
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
			property.IsMissing = true;
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
			property.IsMissing = true;
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
			property.IsMissing = true;
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
			property.IsMissing = true;
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

	#endregion

	#region Struct Property

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
			property.IsMissing = true;
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
			property.IsMissing = true;
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
			property.IsMissing = true;
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
			property.IsMissing = true;
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
			property.IsMissing = true;
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
			property.IsMissing = true;
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
