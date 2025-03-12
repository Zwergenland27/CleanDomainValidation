using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;

namespace Tests.ApplicationTests.Lists.Required;

public class EnumTests
{
    #region From string

	[Fact]
	public void MapEachEnum_ShouldReturnEnumListAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringListIsNotNull()
	{
		//Arrange
		List<string> value = [Helpers.EnumOneString, Helpers.EnumTwoString];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringListParameter(value);
		var property = new RequiredListProperty<RStringListParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeEquivalentTo((List<TestEnum>)[TestEnum.One, TestEnum.Two]);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void MapEachEnum_ShouldReturnNullAndSetInvalidEnumErrorAndRemoveNameFromNameStack_WhenAtLeastOneStringIsInvalidEnum()
	{
		//Arrange
		List<string> value = [Helpers.EnumOneString, Helpers.EnumInvalidString];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringListParameter(value);
		var property = new RequiredListProperty<RStringListParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void MapEachEnum_ShouldReturnNullAndSetMissingErrorAndRemoveNameFromNameStack_WhenStringListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStringListParameter(null);
		var property = new RequiredListProperty<RStringListParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region From int
	
	[Fact]
	public void MapEachEnum_ShouldReturnEnumListAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntListIsNotNull()
	{
		//Arrange
		List<int> value = [Helpers.EnumOneInt, Helpers.EnumTwoInt];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntListParameter(value);
		var property = new RequiredListProperty<RIntListParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

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
		List<int> value = [Helpers.EnumOneInt, Helpers.EnumInvalidInt];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntListParameter(value);
		var property = new RequiredListProperty<RIntListParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void MapEachEnum_ShouldReturnNullAndSetMissingErrorAndRemoveNameFromNameStack_WhenIntListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RIntListParameter(null);
		var property = new RequiredListProperty<RIntListParameter, TestEnum>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}