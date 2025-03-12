using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.ApplicationTests.Classes.Required;

public record RClassParameter(string? Value) : IParameters;

public record RClassValueObject(string Value)
{
	public static CanFail<RClassValueObject> Create(string value)
	{
		if (value == Helpers.ErrorStringValue) return Helpers.ExampleValidationError;
		return new RClassValueObject(value);
	}
}

public record RStructParameter(int? Value) : IParameters;
public record RStructValueObject(int Value)
{
	public static CanFail<RStructValueObject> Create(int value)
	{
		if (value == Helpers.ErrorIntValue) return Helpers.ExampleValidationError;
		return new RStructValueObject(value);
	}
}
