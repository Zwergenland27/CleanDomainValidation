using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;

namespace Tests.ApplicationTests.Lists.Optional;

public class ConstructorMappedTests
{
	#region Class

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new OClassValueObject(v));

		//Assert
		result.ShouldBeEquivalentTo(value.Select(OClassValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, OClassValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new OClassValueObject(v));

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [Helpers.ExampleIntValue, Helpers.AlternateIntValue];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new OStructValueObject(v));

		//Assert
		result.ShouldBeEquivalentTo(value.Select(OStructValueObject.Create).Select(x => x.Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, OStructValueObject>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, v => new OStructValueObject(v));

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}