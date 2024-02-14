using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;

namespace CleanDomainValidation.Application.Extensions;

public static class DirectMapExtensions
{
	#region Class Property
	public static TProperty? Map<TParameters, TProperty>(
		this OptionalClassProperty<TParameters, TProperty> property,
		Func<TParameters, TProperty?> value)
		where TProperty : notnull
	{
		return value.Invoke(property.Parameters);
	}

	public static TProperty Map<TParameters, TProperty>(
		this RequiredClassProperty<TParameters, TProperty> property,
		Func<TParameters, TProperty?> value)
		where TProperty : notnull
	{
		TProperty? rawValue = value.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		return rawValue;
	}

	#endregion

	#region Struct Property

	public static TProperty? Map<TParameters, TProperty>(
		this OptionalStructProperty<TParameters, TProperty> property,
		Func<TParameters, TProperty?> value)
		where TProperty : struct
	{
		return value.Invoke(property.Parameters);
	}

	public static TProperty Map<TParameters, TProperty>(
		this RequiredStructProperty<TParameters, TProperty> property,
		Func<TParameters, TProperty?> value)
		where TProperty : struct
	{
		TProperty? rawValue = value.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		return rawValue.Value;
	}

	#endregion

	#region List Property

	public static IEnumerable<TProperty>? MapEach<TParameters, TProperty>(
		this OptionalListProperty<TParameters, TProperty> property,
		Func<TParameters, IEnumerable<TProperty>?> values)
	{
		return values.Invoke(property.Parameters);
	}

	public static IEnumerable<TProperty> MapEach<TParameters, TProperty>(
	this RequiredListProperty<TParameters, TProperty> property,
	Func<TParameters, IEnumerable<TProperty>?> values)
	{
		IEnumerable<TProperty>? rawValue = values.Invoke(property.Parameters);
		if (rawValue is null)
		{
			property.ValidationResult.Failed(property.MissingError);
			return default!;
		}

		return rawValue;
	}

	#endregion
}
