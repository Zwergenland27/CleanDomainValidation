using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using Shouldly;

namespace Tests.ApplicationTests.Lists.Required;

public class ComplexMappedTests
{
	#region Class

	[Fact]
	public void ComplexMapEachClass_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> value = [Helpers.ExampleStringValue, Helpers.AlternateStringValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var voValue = builder.ClassProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeEquivalentTo(value.Select(v => RClassValueObject.Create(v).Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapEachClass_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStack_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<string> value = [Helpers.ExampleStringValue, Helpers.ErrorStringValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(value);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var voValue = builder.ClassProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapEachClass_ShouldReturnNullAndSetMissingErrorAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassListParameter(null);
		var property = new RequiredListProperty<RClassListParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var voValue = builder.ClassProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
	
	#region Struct
	
	[Fact]
	public void ComplexMapEachStruct_ShouldReturnValueObjectListAndNotSetErrorsAndRemoveNameFromNameStack_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> value = [Helpers.ExampleIntValue, Helpers.AlternateIntValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var voValue = builder.StructProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeEquivalentTo(value.Select(v => RStructValueObject.Create(v).Value).ToList());
		
		property.ValidationResult.HasFailed.ShouldBeFalse();
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ComplexMapEachStruct_ShouldReturnNullAndSetValidationErrorsAndRemoveNameFromNameStac_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<int> value = [Helpers.ExampleIntValue, Helpers.ErrorIntValue];
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(value);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var voValue = builder.StructProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	[Fact]
	public void ComplexMapEachStruct_ShouldReturnNullAndSetMissingErrorAndRemoveNameFromNameStack_WhenParameterListIsNull()
	{
		//Arrange
		var nameStack = new NameStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructListParameter(null);
		var property = new RequiredListProperty<RStructListParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var voValue = builder.StructProperty(p => p.Value)
				.Required()
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(voValue));
		});

		//Assert
		result.ShouldBeNull();
		
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
		
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}
	
	#endregion
}