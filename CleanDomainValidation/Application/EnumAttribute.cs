using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public partial class Validator<TCommand>
{
	protected TEnum RequiredEnum<TEnum>(int? value, Error missingEnumError, Error undefinedEnumError) where TEnum : Enum
	{
		if (value is null)
		{
			_validationErrors.Add(missingEnumError);
			return default!;
		}

		if (!Enum.IsDefined(typeof(TEnum), value))
		{
			_validationErrors.Add(undefinedEnumError);
			return default!;
		}

		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}

	protected TEnum? OptionalEnum<TEnum>(int? value, Error undefinedEnumError) where TEnum : Enum
	{
		if (value is null)
		{
			return default!;
		}

		if (!Enum.IsDefined(typeof(TEnum), value))
		{
			_validationErrors.Add(undefinedEnumError);
			return default!;
		}

		return (TEnum)Enum.ToObject(typeof(TEnum), value);
	}
}
