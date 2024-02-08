using CleanDomainValidation.Application.Class;
using CleanDomainValidation.Application.Struct;

namespace CleanDomainValidation.Application.Extensions;

public static class DirectMapExtensions
{
	public static TProperty? Map<TParameters, TProperty>(this OptionalClassProperty<TParameters, TProperty> property, Func<TParameters, TProperty?> value)
		where TProperty : notnull
	{
		return value.Invoke(property.Parameters);
	}

	public static TProperty Map<TParameters, TProperty>(this RequiredClassProperty<TParameters, TProperty> property, Func<TParameters, TProperty?> value)
		where TProperty : notnull
	{
		var res = value.Invoke(property.Parameters);
		if (res is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		return res;
	}

	public static TProperty? Map<TParameters, TProperty>(this OptionalStructProperty<TParameters, TProperty> property, Func<TParameters, TProperty?> value)
		where TProperty : struct
	{
		return value.Invoke(property.Parameters);
	}

	public static TProperty Map<TParameters, TProperty>(this RequiredStructProperty<TParameters, TProperty> property, Func<TParameters, TProperty?> value)
		where TProperty : struct
	{
		var res = value.Invoke(property.Parameters);
		if (res is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		return res.Value;
	}
}
