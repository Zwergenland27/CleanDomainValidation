using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;
using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Extensions;

public static class CanFailMapExtensions
{
	#region Class Property
	public static TProperty? Map<TParameters, TProperty, TValue>(
		this OptionalClassProperty<TParameters, TProperty> property,
		Func<TParameters, TValue?> value,
		Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : notnull
		where TValue : notnull
	{
		TValue? rawValue = value.Invoke(property.Parameters);

		if (rawValue is null) return default;

		CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawValue);
		if (factoryResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(factoryResult);
			return default;
		}

		return factoryResult.Value;
	}

	public static TProperty? Map<TParameters, TProperty, TValue>(
		this OptionalClassProperty<TParameters, TProperty> property,
		Func<TParameters, TValue?> value,
		Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : notnull
		where TValue : struct
	{
		TValue? rawValue = value.Invoke(property.Parameters);

		if (rawValue is null) return default;

		CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawValue.Value);
		if (factoryResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(factoryResult);
			return default;
		}

		return factoryResult.Value;
	}

	public static TProperty Map<TParameters, TProperty, TValue>(
		this RequiredClassProperty<TParameters, TProperty> property,
		Func<TParameters, TValue?> value,
		Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : notnull
		where TValue : notnull
	{
		TValue? rawValue = value.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawValue);
		if (factoryResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(factoryResult);
			return default!;
		}

		return factoryResult.Value;
	}

	public static TProperty Map<TParameters, TProperty, TValue>(
		this RequiredClassProperty<TParameters, TProperty> property,
		Func<TParameters, TValue?> value,
		Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : notnull
		where TValue : struct
	{
		TValue? rawValue = value.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawValue.Value);
		if (factoryResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(factoryResult);
			return default!;
		}

		return factoryResult.Value;
	}

	#endregion

	#region Struct Property

	public static TProperty? Map<TParameters, TProperty, TValue>(
		this OptionalStructProperty<TParameters, TProperty> property,
		Func<TParameters, TValue?> value,
		Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : struct
		where TValue : notnull
	{
		TValue? rawValue = value.Invoke(property.Parameters);

		if (rawValue is null) return default;

		CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawValue);
		if (factoryResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(factoryResult);
			return default;
		}

		return factoryResult.Value;
	}

	public static TProperty? Map<TParameters, TProperty, TValue>(
		this OptionalStructProperty<TParameters, TProperty> property,
		Func<TParameters, TValue?> value,
		Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : struct
		where TValue : struct
	{
		TValue? rawValue = value.Invoke(property.Parameters);

		if (rawValue is null) return default;

		CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawValue.Value);
		if (factoryResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(factoryResult);
			return default;
		}

		return factoryResult.Value;
	}

	public static TProperty Map<TParameters, TProperty, TValue>(
		this RequiredStructProperty<TParameters, TProperty> property,
		Func<TParameters, TValue?> value,
		Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : struct
		where TValue : notnull
	{
		TValue? rawValue = value.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawValue);
		if (factoryResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(factoryResult);
			return default;
		}

		return factoryResult.Value;
	}

	public static TProperty Map<TParameters, TProperty, TValue>(
		this RequiredStructProperty<TParameters, TProperty> property,
		Func<TParameters, TValue?> value,
		Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : struct
		where TValue : struct
	{
		TValue? rawValue = value.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawValue.Value);
		if (factoryResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(factoryResult);
			return default;
		}

		return factoryResult.Value;
	}

	#endregion

	#region List Property

	public static IEnumerable<TProperty>? MapEach<TParameters, TProperty, TValue>(
		this OptionalListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<TValue>?> values,
		Func<TValue, CanFail<TProperty>> factoryMethod)
	{
		IEnumerable<TValue>? rawValues = values.Invoke(property.Parameters);
		if (rawValues is null) return default;

		List<TProperty> resultProperties = [];
		foreach (var rawProperty in rawValues)
		{
			CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawProperty);
			if (factoryResult.HasFailed)
			{
				property.ValidationResult.InheritFailure(factoryResult);
				continue;
			}

			resultProperties.Add(factoryResult.Value);
		}

		return resultProperties;
	}

	public static IEnumerable<TProperty> MapEach<TParameters, TProperty, TValue>(
		this RequiredListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<TValue>?> values,
		Func<TValue, CanFail<TProperty>> factoryMethod)
	{
		IEnumerable<TValue>? rawValues = values.Invoke(property.Parameters);
		if (rawValues is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		List<TProperty> resultProperties = [];
		foreach (var rawProperty in rawValues)
		{
			CanFail<TProperty> factoryResult = factoryMethod.Invoke(rawProperty);
			if (factoryResult.HasFailed)
			{
				property.ValidationResult.InheritFailure(factoryResult);
				continue;
			}

			resultProperties.Add(factoryResult.Value);
		}

		return resultProperties;
	}

	#endregion
}
