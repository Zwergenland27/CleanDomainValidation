using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public partial class Validator<TResult>
{
	protected TEnum RequiredEnum<TEnum>(int? value, Error missingEnumError, Error undefinedEnumError) where TEnum : Enum
	{
		if (value is null)
		{
			_validationResult.Failed(missingEnumError);
			return default!;
		}

		if (!Enum.IsDefined(typeof(TEnum), value.Value))
		{
			_validationResult.Failed(undefinedEnumError);
			return default!;
		}

		return (TEnum)Enum.ToObject(typeof(TEnum), value.Value);
	}

	protected TEnum RequiredEnum<TEnum>(string? value, Error missingEnumError, Error undefinedEnumError) where TEnum : struct
	{
		if (value is null)
		{
			_validationResult.Failed(missingEnumError);
			return default!;
		}

		if (!Enum.TryParse(value, out TEnum result))
		{
			_validationResult.Failed(undefinedEnumError);
			return default!;
		}

		return result;
	}

	protected TEnum? OptionalEnum<TEnum>(int? value, Error undefinedEnumError) where TEnum : Enum
	{
		if (value is null)
		{
			return default!;
		}

		if (!Enum.IsDefined(typeof(TEnum), value.Value))
		{
			_validationResult.Failed(undefinedEnumError);
			return default!;
		}

		return (TEnum)Enum.ToObject(typeof(TEnum), value.Value);
	}

	protected TEnum? OptionalEnum<TEnum>(string? value, Error undefinedEnumError) where TEnum : struct
	{
		if (value is null)
		{
			return default!;
		}

		if (!Enum.TryParse(value, out TEnum result))
		{
			_validationResult.Failed(undefinedEnumError);
			return default!;
		}

		return result;
	}
}
