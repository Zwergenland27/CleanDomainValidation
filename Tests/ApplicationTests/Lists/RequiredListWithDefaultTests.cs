using CleanDomainValidation.Application.Extensions;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Domain;
using FluentAssertions;

namespace Tests.ApplicationTests.Lists;

public class RequiredListWithDefaultTests
{
    private static Error InvalidEnumError => Error.Validation("Enum.Invalid", "The enum is invalid");
    
	#region Direct Mapped

	[Fact]
	public void DirectMapEachClass_ShouldReturnList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> defaultList = ["default1", "default2"];
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, string>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeEquivalentTo(value);
	}

	[Fact]
	public void DirectMapEachStruct_ShouldReturnList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> defaultList = [3, 4];
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, int>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeEquivalentTo(value);
	}

	[Fact]
	public void DirectMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<string> defaultList = ["default1", "default2"];
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, string>(parameters,defaultList);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<int> defaultList = [3, 4];
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, int>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMapEachClass_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<string> defaultList = ["default1", "default2"];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, string>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void DirectMapEachStruct_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<int> defaultList = [3, 4];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, int>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void DirectMapEachClass_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		List<string> defaultList = ["default1", "default2"];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, string>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void DirectMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		List<int> defaultList = [3, 4];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, int>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Factory Mapped

	[Fact]
	public void FactoryMapEachClass_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		result.Should().BeEquivalentTo(value.Select(RClassValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		result.Should().BeEquivalentTo(value.Select(RStructValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachClass_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		List<string> value = ["value1", "error"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		List<int> value = [1, 9];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void FactoryMapEachClass_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void FactoryMapEachClass_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RClassValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void FactoryMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, RStructValueObject.Create);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Constructor Mapped

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, v => new RClassValueObject(v));

		//Assert
		result.Should().BeEquivalentTo(value.Select(RClassValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, v => new RStructValueObject(v));

		//Assert
		result.Should().BeEquivalentTo(value.Select(RStructValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void ConstructorMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ConstructorMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ConstructorMapEachClass_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, v => new RClassValueObject(v));

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void ConstructorMapEachStruct_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, v => new RStructValueObject(v));

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void ConstructorMapEachClass_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, v => new RClassValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ConstructorMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, v => new RStructValueObject(v));

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Complex Mapped

	[Fact]
	public void ComplexMapEachClass_ShouldReturnValueObjectList_WhenParameterListIsNotNull()	
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		result.Should().BeEquivalentTo(value.Select(RClassValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldReturnValueObjectList_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		result.Should().BeEquivalentTo(value.Select(RStructValueObject.Create).Select(x => x.Value));
	}

	[Fact]
	public void ComplexMapEachClass_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		List<string> value = ["value1", "value2"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNotNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		List<int> value = [1, 2];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachClass_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		List<string> value = ["value1", "error"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldSetErrors_WhenAtLeastOneValueObjectCreationFailed()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		List<int> value = [1, 9];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(Error.Validation("Validation.Error", "An error occured"));
	}

	[Fact]
	public void ComplexMapEachClass_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldReturnDefaultList_WhenParameterListIsNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void ComplexMapEachClass_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		List<RClassValueObject> defaultList = [new ("default1"), new ("default2")];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RClassValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.ClassProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RClassValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void ComplexMapEachStruct_ShouldNotSetErrors_WhenParameterListIsNull()
	{
		//Arrange
		List<RStructValueObject> defaultList = [new (3), new (4)];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RStructValueObject>(parameters, defaultList);

		//Act
		var result = property.MapEachComplex(p => p.Value, builder =>
		{
			var value = builder.StructProperty(p => p.Value)
				.Required(Error.Validation("Error.Missing", "missing error"))
				.Map(r => r);
			return builder.Build(() => RStructValueObject.Create(value));
		});

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion

	#region Enums

	[Fact]
	public void MapEachEnum_ShouldReturnEnumList_WhenStringListIsNotNull()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		List<string> value = ["One", "Two"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		result.Should().BeEquivalentTo([RTestEnum.One, RTestEnum.Two]);
	}

	[Fact]
	public void MapEachEnum_ShouldReturnEnumList_WhenIntListIsNotNull()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		List<int> value = [0, 1];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		result.Should().BeEquivalentTo([RTestEnum.One, RTestEnum.Two]);
	}

	[Fact]
	public void MapEachEnum_ShouldNotSetErrors_WhenStringListIsNotNull()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		List<string> value = ["One", "Two"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void MapEachEnum_ShouldNotSetErrors_WhenIntListIsNotNull()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		List<int> value = [0, 1];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void MapEachEnum_ShouldSetInvalidEnumError_WhenAtLeasOneStringIsInvalidEnum()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		List<string> value = ["One", "Four"];
		var parameters = new RClassListParameter(value);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(InvalidEnumError);
	}

	[Fact]
	public void MapEachEnum_ShouldSetInvalidEnumError_WhenAtLeasOneIntIsInvalidEnum()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		List<int> value = [0, 3];
		var parameters = new RStructListParameter(value);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeTrue();
		property.ValidationResult.Errors.Should().ContainSingle().Which.Should().Be(InvalidEnumError);
	}

	[Fact]
	public void MapEachEnum_ShouldReturnDefaultList_WhenStringListIsNull()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void MapEachEnum_ShouldReturnDefaultList_WhenIntListIsNull()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		result.Should().BeEquivalentTo(defaultList);
	}

	[Fact]
	public void MapEachEnum_ShouldNotSetErrors_WhenStringListIsNull()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		var parameters = new RClassListParameter(null);
		var property = new RequiredListWithDefaultProperty<RClassListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	[Fact]
	public void MapEachEnum_ShouldNotSetErrors_WhenIntListIsNull()
	{
		//Arrange
		List<RTestEnum> defaultList = [RTestEnum.Two, RTestEnum.Three];
		var parameters = new RStructListParameter(null);
		var property = new RequiredListWithDefaultProperty<RStructListParameter, RTestEnum>(parameters, defaultList);

		//Act
		var result = property.MapEach(p => p.Value, InvalidEnumError);

		//Assert
		property.ValidationResult.HasFailed.Should().BeFalse();
	}

	#endregion
}