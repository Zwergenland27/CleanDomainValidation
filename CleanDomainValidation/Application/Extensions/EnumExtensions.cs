using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Extensions;

public static class EnumExtensions
{
	public static TProperty? Map<TParameters, TProperty>(this OptionalEnumProperty<TParameters, TProperty> property, Func<TParameters, string?> value, Error invalidEnumError)
		where TProperty : struct
	{
		string? res = value.Invoke(property.Parameters);
		if(res is null)
		{
			return default;
		}

		if(!Enum.TryParse(res, out TProperty enumResult))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default;
		}

		return enumResult;
	}

	public static TProperty Map<TParameters, TProperty>(this RequiredEnumProperty<TParameters, TProperty> property, Func<TParameters, string?> value, Error invalidEnumError)
		where TProperty : struct
	{
		string? res = value.Invoke(property.Parameters);
		if (res is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		if (!Enum.TryParse(res, out TProperty enumResult))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default!;
		}

		return enumResult;
	}

	public static TProperty? Map<TParameters, TProperty>(this OptionalEnumProperty<TParameters, TProperty> property, Func<TParameters, int?> value, Error invalidEnumError)
	where TProperty : struct
	{
		int? res = value.Invoke(property.Parameters);
		if (res is null)
		{
			return default;
		}

		if (!Enum.IsDefined(typeof(TProperty), res.Value))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default;
		}

		return (TProperty) Enum.ToObject(typeof(TProperty), res.Value);
	}

	public static TProperty Map<TParameters, TProperty>(this RequiredEnumProperty<TParameters, TProperty> property, Func<TParameters, int?> value, Error invalidEnumError)
		where TProperty : struct
	{
		int? res = value.Invoke(property.Parameters);
		if (res is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		if (!Enum.IsDefined(typeof(TProperty), res.Value))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default!;
		}

		return (TProperty)Enum.ToObject(typeof(TProperty), res.Value);
	}
}
