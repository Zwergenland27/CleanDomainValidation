﻿using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.DomainTests;

public class CanFailOfTTests
{
	private readonly Error _exampleError = Error.Conflict("Code", "Message");

	#region AbstractCanFail

	[Fact]
	public void Constructor_Should_ConstructNonFail()
	{
		//Act
		CanFail<string> result = new();

		//Assert
		result.HasFailed.ShouldBeFalse();
		result.Type.ShouldBe(FailureType.None);
	}

	[Fact]
	public void Errors_Should_ThrowNoErrorsOccuredException_When_NonFail()
	{
		//Arrange
		CanFail<string> result = new();

		//Act & Assert
		Should.Throw<NoErrorsOccuredException>(() => result.Errors);
	}

	[Fact]
	public void Failed_Should_AddError()
	{
		//Arrange
		CanFail<string> result = new();

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
		CanFail<string> result = new();

		//Act
		result.Failed(_exampleError);

		//Assert
		result.Type.ShouldBe(FailureType.One);
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
		result.Type.ShouldBe(FailureType.Many);
	}

	[Fact]
	public void ManyFailedOfDifferentType_Should_SetManyDifferentFailureType()
	{
		//Arrange
		CanFail<string> result = new();
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
		CanFail<string> result = new();

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

		CanFail<string> result = new();

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
		CanFail<string> result = new();
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
	public void ValueGet_Should_ThrowValueNotSetException_When_ValueNotSet()
	{
		//Arrange
		CanFail<string> result = new();

		//Act & Assert
		Should.Throw<ValueNotSetException>(() => result.Value);
	}

	[Fact]
	public void ValueGet_Should_ThrowValueInvalidException_When_ErrorsOccured()
	{
		//Arrange
		CanFail<string> result = new();
		result.Failed(_exampleError);

		//Act & Assert
		Should.Throw<ValueInvalidException>(() => result.Value);
	}

	[Fact]
	public void ValueGet_InvalidException_Should_Contain_ErrorCodes()
	{
		//Arrange
		CanFail<string> result = new();
		result.Failed(_exampleError);
		
		//Act & Assert
		Should.Throw<ValueInvalidException>(() => result.Value)
			.Message.ShouldContain(_exampleError.Code);
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
		result.HasFailed.ShouldBeFalse();
		result.Value.ShouldBe(value);

	}

	[Fact]
	public void SuccessFactory_Should_ReturnNonFail()
	{
		//Arrange
		string value = "value";

		//Act
		CanFail<string> result = CanFail<string>.Success(value);

		//Assert
		result.HasFailed.ShouldBeFalse();
		result.Value.ShouldBe(value);
	}

	[Fact]
	public void FromErrorFactory_Should_ReturnFailWithError()
	{
		//Act
		CanFail<string> result = CanFail<string>.FromError(_exampleError);

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
		CanFail<string> result = CanFail<string>.FromErrors(resultOne.Errors);

		//Assert
		result.Errors.Count.ShouldBe(1);
		result.Errors.ShouldContain(_exampleError);
	}

	[Fact]
	public void ImplicitFromError_Should_ReturnFailWithError()
	{
		//Act
		CanFail<string> result = _exampleError;

		//Assert
		result.Errors.Count.ShouldBe(1);
		result.Errors.ShouldContain(_exampleError);
	}

	[Fact]
	public void ImplicitFromValue_Should_ReturnSuccessWithValue()
	{
		//Arrange
		string value = "value";

		//Act
		CanFail<string> result = value;

		//Assert
		result.HasFailed.ShouldBeFalse();
		result.Value.ShouldBe(value);
	}

	[Fact]
	public void ImplicitFromErrors_Should_ReturnFailWithError()
	{
		//Arrange
		CanFail<int> resultOne = new();
		resultOne.Failed(_exampleError);

		//Act
		CanFail<string> result = resultOne.Errors;

		//Assert
		result.Errors.Count.ShouldBe(1);
		result.Errors.ShouldContain(_exampleError);
	}
}
