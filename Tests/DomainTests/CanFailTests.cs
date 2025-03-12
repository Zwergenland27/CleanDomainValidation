using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.DomainTests;

public class CanFailTests
{
	private readonly Error _exampleError = Error.Conflict("Code", "Message");

	#region AbstractCanFail

	[Fact]
	public void Constructor_Should_ConstructNonFail()
	{
		//Act
		CanFail result = new();

		//Assert
		result.HasFailed.ShouldBeFalse();
		result.Type.ShouldBe(FailureType.None);
	}

	[Fact]
	public void Errors_Should_ThrowNoErrorsOccuredException_When_NonFail()
	{
		//Arrange
		CanFail result = new();

		//Act & Assert
		Should.Throw<NoErrorsOccuredException>(() => result.Errors);
	}

	[Fact]
	public void Failed_Should_AddError()
	{
		//Arrange
		CanFail result = new();

		//Act
		result.Failed(_exampleError);

		//Assert
		result.Errors.Count.ShouldBe(1);
		result.Errors.ShouldContain(_exampleError);
		result.HasFailed.ShouldBeTrue();
	}

	[Fact]
	public void SingleFailed_Should_SetOneFailureType()
	{
		//Arrange
		CanFail result = new();

		//Act
		result.Failed(_exampleError);

		//Assert
		result.Type.ShouldBe(FailureType.One);
	}

	[Fact]
	public void ManyFailedOfSameType_Should_SetManyFailureType()
	{
		//Arrange
		CanFail result = new();

		//Act
		result.Failed(_exampleError);
		result.Failed(_exampleError);

		//Assert
		result.Type.ShouldBe(FailureType.Many);
	}

	[Fact]
	public void ManyFailedOfDifferentType_Should_SetManyDifferentFailureType()
	{
		//Arrange
		CanFail result = new();
		Error differentError = Error.Validation("Code", "Message");

		//Act
		result.Failed(_exampleError);
		result.Failed(differentError);

		//Assert
		result.Type.ShouldBe(FailureType.ManyDifferent);
	}

	[Fact]
	public void InheritFailure_Should_AddNoErrors_When_NoErrorsOccured()
	{
		//Arrange
		CanFail resultOne = new();
		CanFail result = new();

		//Act
		result.InheritFailure(resultOne);

		//Assert
		result.HasFailed.ShouldBeFalse();
	}

	[Fact]
	public void InheritFailure_Should_AddErrors_When_ErrorsOccured()
	{
		//Arrange
		CanFail resultOne = new();
		resultOne.Failed(_exampleError);

		CanFail result = new();

		//Act
		result.InheritFailure(resultOne);

		//Assert
		result.Errors.Count.ShouldBe(1);
		result.Errors.ShouldContain(_exampleError);
		result.HasFailed.ShouldBeTrue();
	}

	[Fact]
	public void InheritFailure_Should_AddErrors_When_ErrorsOccured_And_ErrorsAlreadyExisted()
	{
		//Arrange
		CanFail resultOne = new();
		resultOne.Failed(_exampleError);

		Error differentError = Error.Conflict("Code", "Message");
		CanFail result = new();
		result.Failed(differentError);

		//Act
		result.InheritFailure(resultOne);

		//Assert
		result.Errors.Count.ShouldBe(2);
		result.Errors.ShouldContain(_exampleError);
		result.Errors.ShouldContain(differentError);
		result.HasFailed.ShouldBeTrue();
	}

	#endregion

	[Fact]
	public void SuccessFactory_Should_ReturnNonFail()
	{
		//Act
		CanFail result = CanFail.Success;

		//Assert
		result.HasFailed.ShouldBeFalse();
	}

	[Fact]
	public void FromErrorFactory_Should_ReturnFailWithError()
	{
		//Act
		CanFail result = CanFail.FromError(_exampleError);

		//Assert
		result.Errors.Count.ShouldBe(1);
		result.Errors.ShouldContain(_exampleError);
	}

	[Fact]
	public void FromErrorsFactory_Should_ReturnFailWithError()
	{
		//Arrange
		CanFail resultOne = new();
		resultOne.Failed(_exampleError);

		//Act
		CanFail result = CanFail.FromErrors(resultOne.Errors);

		//Assert
		result.Errors.Count.ShouldBe(1);
		result.Errors.ShouldContain(_exampleError);
	}

	[Fact]
	public void ImplicitFromError_Should_ReturnFailWithError()
	{
		//Act
		CanFail result = _exampleError;

		//Assert
		result.Errors.Count.ShouldBe(1);
		result.Errors.ShouldContain(_exampleError);
	}

	[Fact]
	public void ImplicitFromErrors_Should_ReturnFailWithError()
	{
		//Arrange
		CanFail resultOne = new();
		resultOne.Failed(_exampleError);

		//Act
		CanFail result = resultOne.Errors;

		//Assert
		result.Errors.Count.ShouldBe(1);
		result.Errors.ShouldContain(_exampleError);
	}
}
