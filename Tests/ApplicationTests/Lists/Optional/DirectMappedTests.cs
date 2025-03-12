using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;

namespace Tests.ApplicationTests.Lists.Optional;

public class DirectMappedTests
{
	#region Class 

	[Fact]
	public void DirectMapEachClass_ShouldReturnListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, string>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.ShouldBeEquivalentTo(value);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void DirectMapEachClass_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, string>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void DirectMapEachStruct_ShouldReturnListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [Helpers.ExampleIntValue, Helpers.AlternateIntValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, int>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.ShouldBeEquivalentTo(value);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void DirectMapEachStruct_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, int>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}