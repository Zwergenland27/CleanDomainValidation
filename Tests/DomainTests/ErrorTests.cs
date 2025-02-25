using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.DomainTests;

public class ErrorTests
{
	private const string _exampleCode = "Code";
	private const string _exampleMessage = "Message";

	private static void ValidateError(Error error)
	{
		error.Code.ShouldBe(_exampleCode);
		error.Message.ShouldBe(_exampleMessage);
	}

	[Fact]
	public void ConflictFactory_Should_SetConflictType()
	{
		//Act
		Error error = Error.Conflict(_exampleCode, _exampleMessage);

		//Assert
		error.Type.ShouldBe(ErrorType.Conflict);
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
		error.Type.ShouldBe(ErrorType.NotFound);
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
		error.Type.ShouldBe(ErrorType.Validation);
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
	public void ForbiddenFactory_Should_SetConflictType()
	{
		//Act
		Error error = Error.Forbidden(_exampleCode, _exampleMessage);

		//Assert
		error.Type.ShouldBe(ErrorType.Forbidden);
	}

	[Fact]
	public void ForbiddenFactory_Should_SetCodeAndMessage()
	{
		//Act
		Error error = Error.Forbidden(_exampleCode, _exampleMessage);

		//Assert
		ValidateError(error);
	}
}
