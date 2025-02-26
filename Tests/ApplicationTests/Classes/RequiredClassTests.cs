using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Domain;
using Shouldly;

namespace Tests.ApplicationTests.Classes;

public record RClassParameter(string? Value) : IParameters;

public record RClassValueObject(string Value)
{
	public static CanFail<RClassValueObject> Create(string value)
	{
		if (value == Helpers.ErrorStringValue) return Helpers.ExampleValidationError;
		return new RClassValueObject(value);
	}
}

public record RStructParameter(int? Value) : IParameters;
public record RStructValueObject(int Value)
{
	public static CanFail<RStructValueObject> Create(int value)
	{
		if (value == Helpers.ErrorIntValue) return Helpers.ExampleValidationError;
		return new RStructValueObject(value);
	}
}

public class RequiredClassTests
{
	#region Direct Mapped

	[Fact]
	public void DirectMap_ShouldReturnValue_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value);

		//Assert
		validatedProperty.ShouldBe(value);
	}

	[Fact]
	public void DirectMap_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void DirectMap_ShouldRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void DirectMap_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value);

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void DirectMap_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
	}
	
	[Fact]
	public void DirectMap_ShouldRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, string>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion

	#region Factory Mapped

	[Fact]
	public void FactoryMapClass_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		validatedValue.ShouldBe(new RClassValueObject(value));
	}

	[Fact]
	public void FactoryMapStruct_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		validatedValue.ShouldBe(new RStructValueObject(value));
	}

	[Fact]
	public void FactoryMapClass_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}

	[Fact]
	public void FactoryMapStruct_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void FactoryMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void FactoryMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void FactoryMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
	}

	[Fact]
	public void FactoryMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
	}
	
	[Fact]
	public void FactoryMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapClass_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, RClassValueObject.Create);


		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void FactoryMapStruct_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, RStructValueObject.Create);


		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void FactoryMapClass_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
	}

	[Fact]
	public void FactoryMapStruct_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
	}
	
	[Fact]
	public void FactoryMapClass_ShouldRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RClassValueObject.Create);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void FactoryMapStruct_ShouldRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, RStructValueObject.Create);

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion

	#region Constructor Mapped

	[Fact]
	public void ConstructorMapClass_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedValue.ShouldBe(new RClassValueObject(value));
	}

	[Fact]
	public void ConstructorMapStruct_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedValue = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		validatedValue.ShouldBe(new RStructValueObject(value));
	}

	[Fact]
	public void ConstructorMapClass_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}

	[Fact]
	public void ConstructorMapStruct_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void ConstructorMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ConstructorMapStruct_ShouldRemoveNameFromNameStacks_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ConstructorMapClass_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void ConstructorMapStruct_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void ConstructorMapClass_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
	}

	[Fact]
	public void ConstructorMapStruct_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
	}
	
	[Fact]
	public void ConstructorMapClass_ShouldRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RClassValueObject(v));

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ConstructorMapStruct_ShouldRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.Map(p => p.Value, v => new RStructValueObject(v));

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion

	#region Complex Mapped

	[Fact]
	public void ComplexMapClass_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(new RClassValueObject(value));
		});


		//Assert
		validatedProperty.ShouldBe(new RClassValueObject(value));
	}

	[Fact]
	public void ComplexMapStruct_ShouldReturnValueObject_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(new RStructValueObject(value));
		});

		//Assert
		validatedProperty.ShouldBe(new RStructValueObject(value));
	}

	[Fact]
	public void ComplexMapClass_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(new RClassValueObject(value));
		});

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}

	[Fact]
	public void ComplexMapStruct_ShouldNotSetErrors_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(new RStructValueObject(value));
		});

		//Assert
		property.ValidationResult.HasFailed.ShouldBeFalse();
	}
	
	[Fact]
	public void ComplexMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(new RClassValueObject(value));
		});

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNull()
	{
		//Arrange
		var value = Helpers.ExampleIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(new RStructValueObject(value));
		});

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapClass_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleValidationError);
		});

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void ComplexMapStruct_ShouldReturnNull_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleValidationError);
		});

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void ComplexMapClass_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleValidationError);
		});

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
	}

	[Fact]
	public void ComplexMapStruct_ShouldSetErrors_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleValidationError);
		});

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleValidationError);
	}
	
	[Fact]
	public void ComplexMapClass_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorStringValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RClassParameter(value);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleValidationError);
		});

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapStruct_ShouldRemoveNameFromNameStack_WhenValueNotNullAndCreationFailed()
	{
		//Arrange
		var value = Helpers.ErrorIntValue;
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var parameters = new RStructParameter(value);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleValidationError);
		});

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapClass_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleMissingError);
		});

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void ComplexMapStruct_ShouldReturnNull_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		var validatedProperty = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleMissingError);
		});

		//Assert
		validatedProperty.ShouldBe(null);
	}

	[Fact]
	public void ComplexMapClass_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleMissingError);
		});

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
	}

	[Fact]
	public void ComplexMapStruct_ShouldSetMissingError_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleMissingError);
		});

		//Assert
		property.ValidationResult.Errors.Count.ShouldBe(1);
		property.ValidationResult.Errors.ShouldContain(Helpers.ExampleMissingError);
	}
	
	[Fact]
	public void ComplexMapClass_ShouldRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var parameters = new RClassParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RClassParameter, RClassValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RClassValueObject>(Helpers.ExampleMissingError);
		});

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	[Fact]
	public void ComplexMapStruct_ShouldRemoveNameFromNameStack_WhenValueNull()
	{
		//Arrange
		var parameters = new RStructParameter(null);
		var nameStack = new NamingStack("");
		nameStack.PushProperty(Helpers.PropertyName);
		var property = new RequiredClassProperty<RStructParameter, RStructValueObject>(parameters, Helpers.ExampleMissingError, nameStack);

		//Act
		_ = property.MapComplex(p => p.Value, builder =>
		{
			return new ValidatedRequiredProperty<RStructValueObject>(Helpers.ExampleMissingError);
		});

		//Assert
		nameStack.ShouldNotContainPropertyName(new PropertyNameEntry(Helpers.PropertyName));
	}

	#endregion
}
