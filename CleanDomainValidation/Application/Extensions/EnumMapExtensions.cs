using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Extensions;

public static class EnumMapExtensions
{
	#region Enum Property
	public static TProperty? Map<TParameters, TProperty>(
		this OptionalEnumProperty<TParameters, TProperty> property,
		Func<TParameters, string?> value,
		Error invalidEnumError)
		where TProperty : struct
	{
		string? rawEnum = value.Invoke(property.Parameters);
		if(rawEnum is null)
		{
			return default;
		}

		if(!Enum.TryParse(rawEnum, out TProperty enumResult))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default;
		}

		return enumResult;
	}

	public static TProperty Map<TParameters, TProperty>(
		this RequiredEnumProperty<TParameters, TProperty> property,
		Func<TParameters, string?> value,
		Error invalidEnumError)
		where TProperty : struct
	{
		string? rawEnum = value.Invoke(property.Parameters);
		if (rawEnum is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		if (!Enum.TryParse(rawEnum, out TProperty enumResult))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default!;
		}

		return enumResult;
	}

	public static TProperty? Map<TParameters, TProperty>(
		this OptionalEnumProperty<TParameters, TProperty> property,
		Func<TParameters, int?> value,
		Error invalidEnumError)
	where TProperty : struct
	{
		int? rawEnum = value.Invoke(property.Parameters);
		if (rawEnum is null)
		{
			return default;
		}

		if (!Enum.IsDefined(typeof(TProperty), rawEnum.Value))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default;
		}

		return (TProperty) Enum.ToObject(typeof(TProperty), rawEnum.Value);
	}

	public static TProperty Map<TParameters, TProperty>(
		this RequiredEnumProperty<TParameters, TProperty> property,
		Func<TParameters, int?> value,
		Error invalidEnumError)
		where TProperty : struct
	{
		int? rawEnum = value.Invoke(property.Parameters);
		if (rawEnum is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		if (!Enum.IsDefined(typeof(TProperty), rawEnum.Value))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default!;
		}

		return (TProperty)Enum.ToObject(typeof(TProperty), rawEnum.Value);
	}

	#endregion

	#region List Property

	public static IEnumerable<TProperty>? MapEach<TParameters, TProperty>(
		this OptionalListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<string>?> values,
		Error invalidEnumError)
		where TProperty : struct
	{
		IEnumerable<string>? rawEnums = values.Invoke(property.Parameters);
		if (rawEnums is null)
		{
			return default;
		}

		List<TProperty> resultEnums = [];

        foreach (string rawEnum in rawEnums)
        {
			if (!Enum.TryParse(rawEnum, out TProperty enumResult))
			{
				property.ValidationResult.Failed(invalidEnumError);
				continue;
			}

			resultEnums.Add(enumResult);
		}

		return resultEnums;
	}

	public static IEnumerable<TProperty> MapEach<TParameters, TProperty>(
		this RequiredListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<string>?> values,
		Error invalidEnumError)
		where TProperty : struct
	{
		IEnumerable<string>? rawEnums = values.Invoke(property.Parameters);
		if (rawEnums is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		List<TProperty> resultEnums = [];

		foreach (string rawEnum in rawEnums)
		{
			if (!Enum.TryParse(rawEnum, out TProperty enumResult))
			{
				property.ValidationResult.Failed(invalidEnumError);
				continue;
			}

			resultEnums.Add(enumResult);
		}

		return resultEnums;
	}

	public static IEnumerable<TProperty>? MapEach<TParameters, TProperty>(
		this OptionalListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<int>?> values,
		Error invalidEnumError)
		where TProperty : struct
	{
		IEnumerable<int>? rawEnums = values.Invoke(property.Parameters);
		if (rawEnums is null)
		{
			return default;
		}

		List<TProperty> resultEnums = [];

		foreach (int rawEnum in rawEnums)
		{
			if (!Enum.IsDefined(typeof(TProperty), rawEnum))
			{
				property.ValidationResult.Failed(invalidEnumError);
				continue;
			}

			resultEnums.Add((TProperty)Enum.ToObject(typeof(TProperty), rawEnum));
		}

		return resultEnums;
	}

	public static IEnumerable<TProperty> MapEach<TParameters, TProperty>(
		this RequiredListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<int>?> values,
		Error invalidEnumError)
		where TProperty : struct
	{
		IEnumerable<int>? rawEnums = values.Invoke(property.Parameters);
		if (rawEnums is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		List<TProperty> resultEnums = [];

		foreach (int rawEnum in rawEnums)
		{
			if (!Enum.IsDefined(typeof(TProperty), rawEnum))
			{
				property.ValidationResult.Failed(invalidEnumError);
				continue;
			}

			resultEnums.Add((TProperty)Enum.ToObject(typeof(TProperty), rawEnum));
		}

		return resultEnums;
	}

	#endregion
}
