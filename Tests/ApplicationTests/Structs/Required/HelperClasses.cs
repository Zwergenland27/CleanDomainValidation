using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;

namespace Tests.ApplicationTests.Structs.Required;

public record RClassParameter(string? Value) : IParameters;

public record struct RClassValueObject(string Value)
{
	public static CanFail<RClassValueObject> Create(string value)
	{
		if (value == Helpers.ErrorStringValue) return Helpers.ExampleValidationError;
		return new RClassValueObject(value);
	}
}

public record RStructParameter(int? Value) : IParameters;
public record struct RStructValueObject(int Value)
{
	public static CanFail<RStructValueObject> Create(int value)
	{
		if (value == Helpers.ErrorIntValue) return Helpers.ExampleValidationError;
		return new RStructValueObject(value);
	}
}
