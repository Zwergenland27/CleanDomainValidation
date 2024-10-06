using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Extensions;

/// <summary>
/// Extension methods for mapping enum properties
/// </summary>
public static class EnumMapExtensions
{
    #region Enum Property

    /// <summary>
    /// Create the nullable enum property <typeparamref name="TProperty"/> from the string specified in <paramref name="value"/>
    /// </summary>
	/// <param name="property"></param>
	/// <param name="value">String parameter that should be converted to <typeparamref name="TProperty"/></param>
	/// <param name="invalidEnumError">Error that should occur if the enum is invalid</param>
    public static TProperty? Map<TParameters, TProperty>(
		this OptionalEnumProperty<TParameters, TProperty> property,
		Func<TParameters, string?> value,
		Error invalidEnumError)
		where TParameters : notnull
		where TProperty : struct
	{
		string? rawEnum = value.Invoke(property.Parameters);
		if(rawEnum is null)
		{
			return null;
		}

		if(!Enum.TryParse(rawEnum, out TProperty enumResult))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return null;
		}

		return enumResult;
	}

    /// <summary>
    /// Create the non nullable enum property <typeparamref name="TProperty"/> from the string specified in <paramref name="value"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="value">String parameter that should be converted to <typeparamref name="TProperty"/></param>
    /// <param name="invalidEnumError">Error that should occur if the enum is invalid</param>
    public static TProperty Map<TParameters, TProperty>(
		this RequiredEnumProperty<TParameters, TProperty> property,
		Func<TParameters, string?> value,
		Error invalidEnumError)
		where TParameters : notnull
		where TProperty : struct
	{
		string? rawEnum = value.Invoke(property.Parameters);
		if (rawEnum is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default;
		}

		if (!Enum.TryParse(rawEnum, out TProperty enumResult))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default;
		}

		return enumResult;
	}

    /// <summary>
    /// Create the nullable enum property <typeparamref name="TProperty"/> from the integer specified in <paramref name="value"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="value">Integer parameter that should be converted to <typeparamref name="TProperty"/></param>
    /// <param name="invalidEnumError">Error that should occur if the enum is invalid</param>
    public static TProperty? Map<TParameters, TProperty>(
		this OptionalEnumProperty<TParameters, TProperty> property,
		Func<TParameters, int?> value,
		Error invalidEnumError)
		where TParameters : notnull
		where TProperty : struct
	{
		int? rawEnum = value.Invoke(property.Parameters);
		if (rawEnum is null)
		{
			return null;
		}

		if (!Enum.IsDefined(typeof(TProperty), rawEnum.Value))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return null;
		}

		return (TProperty) Enum.ToObject(typeof(TProperty), rawEnum.Value);
	}

    /// <summary>
    /// Create the non nullable enum property <typeparamref name="TProperty"/> from the integer specified in <paramref name="value"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="value">Integer parameter that should be converted to <typeparamref name="TProperty"/></param>
    /// <param name="invalidEnumError">Error that should occur if the enum is invalid</param>
    public static TProperty Map<TParameters, TProperty>(
		this RequiredEnumProperty<TParameters, TProperty> property,
		Func<TParameters, int?> value,
		Error invalidEnumError)
		where TParameters : notnull
		where TProperty : struct
	{
		int? rawEnum = value.Invoke(property.Parameters);
		if (rawEnum is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default;
		}

		if (!Enum.IsDefined(typeof(TProperty), rawEnum.Value))
		{
			property.ValidationResult.Failed(invalidEnumError);
			return default;
		}

		return (TProperty)Enum.ToObject(typeof(TProperty), rawEnum.Value);
	}

    #endregion

    #region List Property

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/>> of the nullable enum list <typeparamref name="TProperty"/> from the strings specified in <paramref name="values"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="values">List of strings parameter that should be converted to <typeparamref name="TProperty"/></param>
    /// <param name="invalidEnumError">Error that should occur if the enum is invalid</param>
    public static IEnumerable<TProperty>? MapEach<TParameters, TProperty>(
		this OptionalListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<string>?> values,
		Error invalidEnumError)
		where TParameters : notnull
		where TProperty : struct
	{
		IEnumerable<string>? rawEnums = values.Invoke(property.Parameters);
		if (rawEnums is null)
		{
			return null;
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

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/>> of the non nullable enum list <typeparamref name="TProperty"/> from the strings specified in <paramref name="values"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="values">List of strings that should be converted to <typeparamref name="TProperty"/></param>
    /// <param name="invalidEnumError">Error that should occur if the enum is invalid</param>
    public static IEnumerable<TProperty> MapEach<TParameters, TProperty>(
		this RequiredListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<string>?> values,
		Error invalidEnumError)
		where TParameters : notnull
		where TProperty : struct
	{
		IEnumerable<string>? rawEnums = values.Invoke(property.Parameters);
		if (rawEnums is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return null!;
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

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/>> of the nullable enum list <typeparamref name="TProperty"/> from the integers specified in <paramref name="values"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="values">List of integers that should be converted to <typeparamref name="TProperty"/></param>
    /// <param name="invalidEnumError">Error that should occur if the enum is invalid</param>
    public static IEnumerable<TProperty>? MapEach<TParameters, TProperty>(
		this OptionalListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<int>?> values,
		Error invalidEnumError)
		where TParameters : notnull
		where TProperty : struct
	{
		IEnumerable<int>? rawEnums = values.Invoke(property.Parameters);
		if (rawEnums is null)
		{
			return null;
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

    /// <summary>
    /// Create each element of type <typeparamref name="TProperty"/>> of the non nullable enum list <typeparamref name="TProperty"/> from the integers specified in <paramref name="values"/>
    /// </summary>
    /// <param name="property"></param>
    /// <param name="values">List of integers that should be converted to <typeparamref name="TProperty"/></param>
    /// <param name="invalidEnumError">Error that should occur if the enum is invalid</param>
    public static IEnumerable<TProperty> MapEach<TParameters, TProperty>(
		this RequiredListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<int>?> values,
		Error invalidEnumError)
		where TParameters : notnull
		where TProperty : struct
	{
		IEnumerable<int>? rawEnums = values.Invoke(property.Parameters);
		if (rawEnums is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return null!;
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
