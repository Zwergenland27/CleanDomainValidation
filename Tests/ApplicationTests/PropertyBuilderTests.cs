using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;

namespace Tests.ApplicationTests;

public record Parameters() : IParameters;
public record Result(
    string RequiredClassProperty,
    string? OptionalClassProperty,
    int RequiredStructProperty,
    int? OptionalStructProperty,
    TestEnum RequiredEnumProperty,
    TestEnum? OptionalEnumProperty,
    List<int> RequiredListProperty,
    List<int>? OptionalListProperty);

public enum TestEnum
{
    One
}

public class TestablePropertyBuilder(Parameters parameters) : PropertyBuilder<Parameters, Result>(parameters)
{
    public new IReadOnlyList<ValidatableProperty> Properties => base.Properties;
}

public class PropertyBuilderTests
{
    [Fact]
    public void ClassProperty_ShouldReturnClassProperty_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.ClassProperty(x => x.RequiredClassProperty);

        //Assert
        builder.Should().BeOfType<ClassProperty<Parameters, string>>();
    }

    [Fact]
    public void ClassProperty_ShouldAddPropertyToList_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.ClassProperty(x => x.RequiredClassProperty);

        //Assert
        propertyBuilder.Properties.Should().Contain(builder);
    }

    [Fact]
    public void StructProperty_ShouldReturnStructProperty_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.StructProperty(x => x.RequiredStructProperty);

        //Assert
        builder.Should().BeOfType<StructProperty<Parameters, int>>();
    }

    [Fact]
    public void StructProperty_ShouldAddPropertyToList_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.StructProperty(x => x.RequiredStructProperty);

        //Assert
        propertyBuilder.Properties.Should().Contain(builder);
    }

    [Fact]
    public void StructProperty_ShouldReturnStructProperty_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.StructProperty(x => x.OptionalStructProperty);

        //Assert
        builder.Should().BeOfType<StructProperty<Parameters, int>>();
    }

    [Fact]
    public void StructProperty_ShouldAddPropertyToList_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.StructProperty(x => x.OptionalStructProperty);

        //Assert
        propertyBuilder.Properties.Should().Contain(builder);
    }

    [Fact]
    public void EnumProperty_ShouldReturnEnumProperty_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty);

        //Assert
        builder.Should().BeOfType<EnumProperty<Parameters, TestEnum>>();
    }

    [Fact]
    public void EnumProperty_ShouldAddPropertyToList_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty);

        //Assert
        propertyBuilder.Properties.Should().Contain(builder);
    }

    [Fact]
    public void EnumProperty_ShouldReturnEnumProperty_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty);

        //Assert
        builder.Should().BeOfType<EnumProperty<Parameters, TestEnum>>();
    }

    [Fact]
    public void EnumProperty_ShouldAddPropertyToList_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty);

        //Assert
        propertyBuilder.Properties.Should().Contain(builder);
    }

    [Fact]
    public void ListProperty_ShouldReturnListProperty_OnRequiredListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.ListProperty(x => x.RequiredListProperty);

        //Assert
        builder.Should().BeOfType<ListProperty<Parameters, int>>();
    }

    [Fact]
    public void ListProperty_ShouldAddPropertyToList_OnRequiredListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var propertyBuilder = new TestablePropertyBuilder(parameters);

        //Act
        var builder = propertyBuilder.ListProperty(x => x.RequiredListProperty);

        //Assert
        propertyBuilder.Properties.Should().Contain(builder);
    }
}
