using CleanDomainValidation.Application.Class;
using CleanDomainValidation.Application.Struct;
using CleanDomainValidation.Domain;

namespace CleanDomainValidation.Application.Extensions;

public static class CanFailMapExtensions
{
	public static TProperty? Map<TParameters, TProperty, TValue>(this OptionalClassProperty<TParameters, TProperty> property, Func<TParameters, TValue?> value, Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : notnull
		where TValue : notnull
	{
		var res = value.Invoke(property.Parameters);

		if(res is null) return default;

		var creationResult = factoryMethod.Invoke(res);
		if (creationResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(creationResult);
			return default;
		}

		return creationResult.Value;
	}

	public static TProperty? Map<TParameters, TProperty, TValue>(this OptionalClassProperty<TParameters, TProperty> property, Func<TParameters, TValue?> value, Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : notnull
		where TValue : struct
	{
		var res = value.Invoke(property.Parameters);

		if (res is null) return default;

		var creationResult = factoryMethod.Invoke(res.Value);
		if (creationResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(creationResult);
			return default;
		}

		return creationResult.Value;
	}

	public static TProperty Map<TParameters, TProperty, TValue>(this RequiredClassProperty<TParameters, TProperty> property, Func<TParameters, TValue?> value, Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : notnull
		where TValue : notnull
	{
		var res = value.Invoke(property.Parameters);
		if (res is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		var creationResult = factoryMethod.Invoke(res);
		if (creationResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(creationResult);
			return default!;
		}

		return creationResult.Value;
	}

	public static TProperty Map<TParameters, TProperty, TValue>(this RequiredClassProperty<TParameters, TProperty> property, Func<TParameters, TValue?> value, Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : notnull
		where TValue : struct
	{
		var res = value.Invoke(property.Parameters);
		if (res is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		var creationResult = factoryMethod.Invoke(res.Value);
		if (creationResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(creationResult);
			return default!;
		}

		return creationResult.Value;
	}

	public static TProperty? Map<TParameters, TProperty, TValue>(this OptionalStructProperty<TParameters, TProperty> property, Func<TParameters, TValue?> value, Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : struct
		where TValue : notnull
	{
		var res = value.Invoke(property.Parameters);

		if(res is null) return default;

		var creationResult = factoryMethod.Invoke(res);
		if (creationResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(creationResult);
			return default;
		}

		return creationResult.Value;
	}

	public static TProperty? Map<TParameters, TProperty, TValue>(this OptionalStructProperty<TParameters, TProperty> property, Func<TParameters, TValue?> value, Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : struct
		where TValue : struct
	{
		var res = value.Invoke(property.Parameters);

		if (res is null) return default;

		var creationResult = factoryMethod.Invoke(res.Value);
		if (creationResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(creationResult);
			return default;
		}

		return creationResult.Value;
	}

	public static TProperty Map<TParameters, TProperty, TValue>(this RequiredStructProperty<TParameters, TProperty> property, Func<TParameters, TValue?> value, Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : struct
		where TValue : notnull
	{
		var res = value.Invoke(property.Parameters);
		if (res is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		var creationResult = factoryMethod.Invoke(res);
		if (creationResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(creationResult);
			return default;
		}

		return creationResult.Value;
	}

	public static TProperty Map<TParameters, TProperty, TValue>(this RequiredStructProperty<TParameters, TProperty> property, Func<TParameters, TValue?> value, Func<TValue, CanFail<TProperty>> factoryMethod)
		where TProperty : struct
		where TValue : struct
	{
		var res = value.Invoke(property.Parameters);
		if (res is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		var creationResult = factoryMethod.Invoke(res.Value);
		if (creationResult.HasFailed)
		{
			property.ValidationResult.InheritFailure(creationResult);
			return default;
		}

		return creationResult.Value;
	}
}
