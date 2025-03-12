using CleanDomainValidation.Application;
using CleanDomainValidation.Domain;

namespace Tests.ApplicationTests.Lists.Optional;

public record OClassListParameter(List<string>? Value) : IParameters;

public record OClassValueObject(string Value)
{
	public static CanFail<OClassValueObject> Create(string value)
	{
		if (value == Helpers.ErrorStringValue) return Helpers.ExampleValidationError;
		return new OClassValueObject(value);
	}
}

public record OStructListParameter(List<int>? Value) : IParameters;

public record OStructValueObject(int Value)
{
	public static CanFail<OStructValueObject> Create(int value)
	{
		if (value == Helpers.ErrorIntValue) return Helpers.ExampleValidationError;
		return new OStructValueObject(value);
	}
}

public record OStringListParameter(List<string>? Value) : IParameters;

public record OIntListParameter(List<int>? Value) : IParameters;
