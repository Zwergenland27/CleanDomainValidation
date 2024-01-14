using CleanDomainValidation.Domain;
using FluentAssertions;
using Xunit.Sdk;

namespace Tests.DomainTests;

public class ErrorTests
{
	private const string _exampleCode = "Code";
	private const string _exampleMessage = "Message";

	private static void ValidateError(Error error)
	{
		error.Code.Should().Be(_exampleCode);
		error.Message.Should().Be(_exampleMessage);
	}

	[Fact]
	public void ConflictFactory_Should_SetConflictType()
	{
		//Act
		Error error = Error.Conflict(_exampleCode, _exampleMessage);

		//Assert
		error.Type.Should().Be(ErrorType.Conflict);
	}

	[Fact]
	public void ConflictFactory_Should_SetCodeAndMessage()
	{
		//Act
		Error error = Error.Conflict(_exampleCode, _exampleMessage);

		//Assert
		ValidateError(error);
	}

	[Fact]
	public void NotFoundFactory_Should_SetConflictType()
	{
		//Act
		Error error = Error.NotFound(_exampleCode, _exampleMessage);

		//Assert
		error.Type.Should().Be(ErrorType.NotFound);
	}

	[Fact]
	public void NotFoundFactory_Should_SetCodeAndMessage()
	{
		//Act
		Error error = Error.NotFound(_exampleCode, _exampleMessage);

		//Assert
		ValidateError(error);
	}

	[Fact]
	public void ValidationFactory_Should_SetConflictType()
	{
		//Act
		Error error = Error.Validation(_exampleCode, _exampleMessage);

		//Assert
		error.Type.Should().Be(ErrorType.Validation);
	}

	[Fact]
	public void ValidationFactory_Should_SetCodeAndMessage()
	{
		//Act
		Error error = Error.Validation(_exampleCode, _exampleMessage);

		//Assert
		ValidateError(error);
	}

	[Fact]
	public void UnexpectedFactory_Should_SetConflictType()
	{
		//Act
		Error error = Error.Unexpected(_exampleCode, _exampleMessage);

		//Assert
		error.Type.Should().Be(ErrorType.Unexpected);
	}

	[Fact]
	public void UnexpectedFactory_Should_SetCodeAndMessage()
	{
		//Act
		Error error = Error.Unexpected(_exampleCode, _exampleMessage);

		//Assert
		ValidateError(error);
	}
}
