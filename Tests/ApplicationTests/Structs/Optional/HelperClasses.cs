using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;

namespace Tests.ApplicationTests.Structs.Optional;

public record OClassParameter(string? Value);

public record struct OClassValueObject(string Value)
{
	public static CanFail<OClassValueObject> Create(string value)
	{
		if (value == Helpers.ErrorStringValue) return Helpers.ExampleValidationError;
		return new OClassValueObject(value);
	}
}

public record OStructParameter(int? Value);
public record struct OStructValueObject(int Value)
{
	public static CanFail<OStructValueObject> Create(int value)
	{
		if (value == Helpers.ErrorIntValue) return Helpers.ExampleValidationError;
		return new OStructValueObject(value);
	}
}
