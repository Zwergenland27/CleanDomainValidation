using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.DomainTests;

public class CanFailOfTTests
{
	private readonly Error _exampleError = Error.Unexpected("Code", "Message");

	#region AbstractCanFail

	[Fact]
	public void Constructor_Should_ConstructNonFail()
	{
		//Act
		CanFail<string> result = new();

		//Assert
		result.HasFailed.Should().BeFalse();
		result.Type.Should().Be(FailureType.None);
	}

	[Fact]
	public void Errors_Should_ThrowNoErrorsOccuredException_When_NonFail()
	{
		//Arrange
		CanFail<string> result = new();

		//Act & Assert
		result.Invoking(r => r.Errors)
			.Should()
			.Throw<NoErrorsOccuredException>();
	}

	[Fact]
	public void Failed_Should_AddError()
	{
		//Arrange
		CanFail<string> result = new();

		//Act
		result.Failed(_exampleError);

		//Assert
		result.Errors.Should().ContainSingle().Which.Should().Be(_exampleError);
		result.HasFailed.Should().BeTrue();
	}

	[Fact]
	public void SingleFailed_Should_SetOneFailureType()
	{
		//Arrange
		CanFail<string> result = new();

		//Act
		result.Failed(_exampleError);

		//Assert
		result.Type.Should().Be(FailureType.One);
	}

	[Fact]
	public void ManyFailedOfSameType_Should_SetManyFailureType()
	{
		//Arrange
		CanFail<string> result = new();

		//Act
		result.Failed(_exampleError);
		result.Failed(_exampleError);

		//Assert
		result.Type.Should().Be(FailureType.Many);
	}

	[Fact]
	public void ManyFailedOfDifferentType_Should_SetManyDifferentFailureType()
	{
		//Arrange
		CanFail<string> result = new();
		Error differentError = Error.Conflict("Code", "Message");

		//Act
		result.Failed(_exampleError);
		result.Failed(differentError);

		//Assert
		result.Type.Should().Be(FailureType.ManyDifferent);
	}

	[Fact]
	public void InheritFailure_Should_AddNoErrors_When_NoErrorsOccured()
	{
		//Arrange
		CanFail resultOne = new();
		CanFail<string> result = new();

		//Act
		result.InheritFailure(resultOne);

		//Assert
		result.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void InheritFailure_Should_AddErrors_When_ErrorsOccured()
	{
		//Arrange
		CanFail resultOne = new();
		resultOne.Failed(_exampleError);

		CanFail<string> result = new();

		//Act
		result.InheritFailure(resultOne);

		//Assert
		result.Errors.Should().ContainSingle().Which.Should().Be(_exampleError);
		result.HasFailed.Should().BeTrue();
	}

	[Fact]
	public void InheritFailure_Should_AddErrors_When_ErrorsOccured_And_ErrorsAlreadyExisted()
	{
		//Arrange
		CanFail resultOne = new();
		resultOne.Failed(_exampleError);

		Error differentError = Error.Conflict("Code", "Message");
		CanFail<string> result = new();
		result.Failed(differentError);

		//Act
		result.InheritFailure(resultOne);

		//Assert
		result.Errors.Count.Should().Be(2);
		result.Errors.Should().Contain(_exampleError);
		result.Errors.Should().Contain(differentError);
		result.HasFailed.Should().BeTrue();
	}

	#endregion
	
	[Fact]
	public void ValueGet_Should_ThrowValueNotSetException_When_ValueNotSet()
	{
		//Arrange
		CanFail<string> result = new();

		//Act & Assert
		result.Invoking(r => r.Value)
			.Should().Throw<ValueNotSetException>();
	}

	[Fact]
	public void ValueGet_Should_ThrowValueInvalidException_When_ErrorsOccured()
	{
		//Arrange
		CanFail<string> result = new();
		result.Failed(_exampleError);

		//Act & Assert
		result.Invoking(r => r.Value)
			.Should().Throw<ValueInvalidException>();
	}

	[Fact]
	public void Succeeded_Should_SetValue_When_NoErrorsOccured()
	{
		//Arrange
		string value = "value";
		CanFail<string> result = new();

		//Act
		result.Succeeded(value);

		//Assert
		result.HasFailed.Should().BeFalse();
		result.Value.Should().Be(value);

	}

	[Fact]
	public void SuccessFactory_Should_ReturnNonFail()
	{
		//Arrange
		string value = "value";

		//Act
		CanFail<string> result = CanFail<string>.Success(value);

		//Assert
		result.HasFailed.Should().BeFalse();
		result.Value.Should().Be(value);
	}

	[Fact]
	public void FromErrorFactory_Should_ReturnFailWithError()
	{
		//Act
		CanFail<string> result = CanFail<string>.FromError(_exampleError);

		//Assert
		result.Errors.Should().ContainSingle().Which.Should().Be(_exampleError);
	}

	[Fact]
	public void FromFailureFactory_Should_ThrowNoErrorsOccuredException_When_ResultNotFail()
	{
		//Arrange
		CanFail resultOne = new();

		//Act & Assert
		FluentActions.Invoking(() => CanFail<string>.FromFailure(resultOne)).Should().Throw<NoErrorsOccuredException>();
	}

	[Fact]
	public void FromFailureFactory_Should_ReturnFailWithError()
	{
		//Arrange
		CanFail resultOne = new();
		resultOne.Failed(_exampleError);

		//Act
		CanFail<string> result = CanFail<string>.FromFailure(resultOne);

		//Assert
		result.Errors.Should().ContainSingle().Which.Should().Be(_exampleError);
	}

	[Fact]
	public void ImplicitFromError_Should_ReturnFailWithError()
	{
		//Act
		CanFail<string> result = _exampleError;

		//Assert
		result.Errors.Should().ContainSingle().Which.Should().Be(_exampleError);
	}

	[Fact]
	public void ImplicitFromValue_Should_ReturnSuccessWithValue()
	{
		//Arrange
		string value = "value";

		//Act
		CanFail<string> result = CanFail<string>.Success(value);

		//Assert
		result.HasFailed.Should().BeFalse();
		result.Value.Should().Be(value);
	}
}
