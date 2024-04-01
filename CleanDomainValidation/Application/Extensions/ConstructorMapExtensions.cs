using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;
using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Extensions;

public static class ConstructorMapExtensions
{
	#region Class Property
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
			property.IsMissing = true;
			return null;
		}

		return constructor.Invoke(rawValue);
	}

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
			property.IsMissing = true;
			return null;
		}

		return constructor.Invoke(rawValue.Value);
	}

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
			property.IsMissing = true;
			property.ValidationResult.Failed(property.MissingError);
			return null!;
		}

		return constructor.Invoke(rawValue);
	}

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
			property.IsMissing = true;
			property.ValidationResult.Failed(property.MissingError);
			return null!;
		}

		return constructor.Invoke(rawValue.Value);
	}

	#endregion

	#region Struct Property

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
			property.IsMissing = true;
			return null;
		}

		return constructor.Invoke(rawValue);
	}

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
			property.IsMissing = true;
			return null;
		}

		return constructor.Invoke(rawValue.Value);
	}

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
			property.IsMissing = true;
			property.ValidationResult.Failed(property.MissingError);
			return default;
		}

		return constructor.Invoke(rawValue);
	}

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
			property.IsMissing = true;
			property.ValidationResult.Failed(property.MissingError);
			return default;
		}

		return constructor.Invoke(rawValue.Value);
	}

	#endregion

	#region List Property

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
			property.IsMissing = true;
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
			property.IsMissing = true;
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
