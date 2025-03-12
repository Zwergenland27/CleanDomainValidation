using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;
using Tests.ApplicationTests.Lists.Required;

namespace Tests.ApplicationTests.Lists.WithDefault;

public class EnumTests
{
    #region From string

	[Fact]
	public void MapEachEnum_ShouldReturnEnumListAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringListIsNotNull()
	{
		//Arrange
		List<TestEnum> defaultList = [Helpers.DefaultEnumValue, Helpers.DefaultEnumAlternateValue];
		List<string> value = [Helpers.EnumOneString, Helpers.EnumTwoString];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, TestEnum>(parameters, defaultList, nameStack);

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
		List<TestEnum> defaultList = [Helpers.DefaultEnumValue, Helpers.DefaultEnumAlternateValue];
		List<string> value = [Helpers.EnumOneString, Helpers.EnumInvalidString];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, TestEnum>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void MapEachEnum_ShouldReturnDefaultListAndNotSetErrorsAndRemoveNameFromNameStack_WhenStringListIsNull()
	{
		//Arrange
		List<TestEnum> defaultList = [Helpers.DefaultEnumValue, Helpers.DefaultEnumAlternateValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, TestEnum>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeEquivalentTo(defaultList);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	

	#endregion
	
	#region From int
	
	[Fact]
	public void MapEachEnum_ShouldReturnEnumListAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntListIsNotNull()
	{
		//Arrange
		List<TestEnum> defaultList = [Helpers.DefaultEnumValue, Helpers.DefaultEnumAlternateValue];
		List<int> value = [Helpers.EnumOneInt, Helpers.EnumTwoInt];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, TestEnum>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeEquivalentTo((List<TestEnum>)[TestEnum.One, TestEnum.Two]);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void MapEachEnum_ShouldReturnNullAndSetInvalidEnumErrorAndRemoveNameFromNameStack_WhenAtLeastOneIntIsInvalidEnum()
	{
		//Arrange
		List<TestEnum> defaultList = [Helpers.DefaultEnumValue, Helpers.DefaultEnumAlternateValue];
		List<int> value = [Helpers.EnumOneInt, Helpers.EnumInvalidInt];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, TestEnum>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleInvalidEnumError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void MapEachEnum_ShouldReturnDefaultListAndNotSetErrorsAndRemoveNameFromNameStack_WhenIntListIsNull()
	{
		//Arrange
		List<TestEnum> defaultList = [Helpers.DefaultEnumValue, Helpers.DefaultEnumAlternateValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, TestEnum>(parameters, defaultList, nameStack);

		//Act
		var result = property.MapEach(p => p.Value, Helpers.ExampleInvalidEnumError);

		//Assert
		result.ShouldBeEquivalentTo(defaultList);
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}