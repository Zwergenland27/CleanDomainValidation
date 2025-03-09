using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;

namespace Tests.ApplicationTests.Lists.Optional;

public class EnumTests
{
	#region From string

	[Fact]
	public void MapEachEnum_ShouldReturnEnumListAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringListIsNotNull()
	{
		//Arrange
		List<string> value = [Helpers.EnumOneString, Helpers.EnumTwoString];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, TestEnum>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeEquivalentTo((List<TestEnum>) [TestEnum.One, TestEnum.Two]);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void MapEachEnum_ShouldReturnNullAndSetInvalidEnumErrorAndRemoveNameFromNameStack_WhenAtLeastOneStringIsInvalidEnum()
	{
		//Arrange
		List<string> value = [Helpers.EnumOneString, Helpers.EnumInvalidString];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(value);
		var property = new OptionalListProperty<OClassListParameter, TestEnum>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void MapEachEnum_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringListIsNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OClassListParameter(null);
		var property = new OptionalListProperty<OClassListParameter, TestEnum>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region From int
	
	[Fact]
	public void MapEachEnum_ShouldReturnEnumListAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntListIsNotNull()
	{
		//Arrange
		List<int> value = [Helpers.EnumOneInt, Helpers.EnumTwoInt];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, TestEnum>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeEquivalentTo((List<TestEnum>) [TestEnum.One, TestEnum.Two]);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void MapEachEnum_ShouldReturnNullAndSetInvalidEnumErrorAndRemoveNameFromNameStack_WhenAtLeastOneIntIsInvalidEnum()
	{
		//Arrange
		List<int> value = [0, 3];
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(value);
		var property = new OptionalListProperty<OStructListParameter, TestEnum>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void MapEachEnum_ShouldReturnNullAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntListIsNull()
	{
		//Arrange
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new OStructListParameter(null);
		var property = new OptionalListProperty<OStructListParameter, TestEnum>(parameters, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}