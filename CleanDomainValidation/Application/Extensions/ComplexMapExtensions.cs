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
		Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
		where TProperty : notnull
		where TPropertyParameters : notnull
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null) return default;

		var builder = new PropertyBuilder<TPropertyParameters, TProperty>(builderParameters);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return default;
		}

		return buildResult.Value;
	}

	public static TProperty? MapComplex<TParameters, TProperty, TPropertyParameters>(
		this OptionalClassProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
		where TProperty : notnull
		where TPropertyParameters : struct
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null) return default;

		var builder = new PropertyBuilder<TPropertyParameters, TProperty>(builderParameters.Value);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return default;
		}

		return buildResult.Value;
	}

	public static TProperty MapComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredClassProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
		where TProperty : notnull
		where TPropertyParameters: notnull
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		var builder = new PropertyBuilder<TPropertyParameters, TProperty>(builderParameters);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return default!;
		}

		return buildResult.Value;
	}

	public static TProperty MapComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredClassProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
		where TProperty : notnull
		where TPropertyParameters: struct
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		var builder = new PropertyBuilder<TPropertyParameters, TProperty>(builderParameters.Value);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return default!;
		}

		return buildResult.Value;
	}

	#endregion

	#region Struct Property

	public static TProperty? MapComplex<TParameters, TProperty, TPropertyParameters>(
	this OptionalStructProperty<TParameters, TProperty> property,
	Func<TParameters, TPropertyParameters?> propertyParameters,
	Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
	where TProperty : struct
	where TPropertyParameters : notnull
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null) return default;

		var builder = new PropertyBuilder<TPropertyParameters, TProperty>(builderParameters);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return default;
		}

		return buildResult.Value;
	}

	public static TProperty? MapComplex<TParameters, TProperty, TPropertyParameters>(
		this OptionalStructProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
		where TProperty : struct
		where TPropertyParameters : struct
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null) return default;

		var builder = new PropertyBuilder<TPropertyParameters, TProperty>(builderParameters.Value);
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
		Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
		where TProperty : struct
		where TPropertyParameters : notnull
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		var builder = new PropertyBuilder<TPropertyParameters, TProperty>(builderParameters);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return default!;
		}

		return buildResult.Value;
	}

	public static TProperty MapComplex<TParameters, TProperty, TPropertyParameters>(
		this RequiredStructProperty<TParameters, TProperty> property,
		Func<TParameters, TPropertyParameters?> propertyParameters,
		Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
		where TProperty : struct
		where TPropertyParameters : struct
	{
		TPropertyParameters? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		var builder = new PropertyBuilder<TPropertyParameters, TProperty>(builderParameters.Value);
		var buildResult = propertyBuilder.Invoke(builder).Build();
		if (buildResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(buildResult);
			return default!;
		}

		return buildResult.Value;
	}

	#endregion

	#region List Property

	public static IEnumerable<TProperty>? MapEachComplex<TParameters, TProperty, TPropertyParameters>(
		this OptionalListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<TPropertyParameters>?> propertyParameters,
		Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
	{
		IEnumerable<TPropertyParameters>? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null) return default;

		List<TProperty> resultProperties = [];
		foreach(var rawProperty in builderParameters)
		{
			var builder = new PropertyBuilder<TPropertyParameters, TProperty>(rawProperty);
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
		Func<PropertyBuilder<TPropertyParameters, TProperty>, ValidatedProperty<TProperty>> propertyBuilder)
	{
		IEnumerable<TPropertyParameters>? builderParameters = propertyParameters.Invoke(property.Parameters);
		if (builderParameters is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		List<TProperty> resultProperties = [];
		foreach (var rawProperty in builderParameters)
		{
			var builder = new PropertyBuilder<TPropertyParameters, TProperty>(rawProperty);
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
