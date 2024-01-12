using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application;

public partial class Validator<TCommand>
{
	protected TValueObject? OptionalAttribute<T1, TValueObject>(
		T1? param1,
		Func<T1, TValueObject> createMethod)
		where T1 : notnull
	{
		if (param1 is null)
		{
			return default;
		}

		return createMethod.Invoke(param1);
	}

	protected TValueObject? OptionalAttribute<T1, TValueObject>(
		T1? param1,
		Func<T1, TValueObject> createMethod)
		where T1 : struct
	{
		if (param1 is null)
		{
			return default;
		}

		return createMethod.Invoke(param1.Value);
	}

	protected TValueObject? OptionalAttribute<T1, TValueObject>(
		T1? param1,
		Func<T1, CanFail<TValueObject>> createMethod)
		where T1 : notnull
	{
		if (param1 is null)
		{
			return default;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1);
		if (result.HasFailed)
		{
			_validationErrors.AddRange(result.Errors);
			return default!;
		}

		return result.Value;
	}

	protected TValueObject? OptionalAttribute<T1, TValueObject>(
		T1? param1,
		Func<T1, CanFail<TValueObject>> createMethod)
		where T1 : struct
	{
		if (param1 is null)
		{
			return default;
		}

		CanFail<TValueObject> result = createMethod.Invoke(param1.Value);
		if (result.HasFailed)
		{
			_validationErrors.AddRange(result.Errors);
			return default!;
		}

		return result.Value;
	}
}
