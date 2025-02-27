using CleanDomainValidation.Application;
using CleanDomainValidation.Application.Classes;
using CleanDomainValidation.Application.Enums;
using CleanDomainValidation.Application.Lists;
using CleanDomainValidation.Application.Structs;
using Shouldly;

namespace Tests.ApplicationTests;

public record Parameters() : IParameters;
public record Result(
    string RequiredClassProperty,
    int RequiredStructProperty,
    int? OptionalStructProperty,
    TestEnum RequiredEnumProperty,
    TestEnum? OptionalEnumProperty,
    List<int> RequiredListProperty);

public class TestablePropertyBuilder(Parameters parameters, NamingStack namingStack) : PropertyBuilder<Parameters, Result>(parameters, namingStack)
{
    public new IReadOnlyList<ValidatableProperty> Properties => base.Properties;
}

public class PropertyBuilderTests
{
    public static string PropertyName => "PropertyName";
    
    [Fact]
    public void ClassProperty_ShouldReturnClassProperty_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        var builder = propertyBuilder.ClassProperty(x => x.RequiredClassProperty);

        //Assert
        builder.ShouldBeOfType<ClassProperty<Parameters, string>>();
    }

    [Fact]
    public void ClassProperty_ShouldAddPropertyToList_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        var builder = propertyBuilder.ClassProperty(x => x.RequiredClassProperty);

        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }

    [Fact]
    public void ClassProperty_ShouldPushPropertyNameToNameStack_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        _ = propertyBuilder.ClassProperty(x => x.RequiredClassProperty);

        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredClassProperty"));
    }
    
    [Fact]
    public void ClassProperty_ShouldPushCustomNameToNameStack_OnClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        _ = propertyBuilder.ClassProperty(x => x.RequiredClassProperty, PropertyName);

        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }
    
    [Fact]
    public void ClassProperty_ShouldPushCustomNameToNameStack_OnManualTypedClassProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);

        //Act
        _ = propertyBuilder.ClassProperty<string>(PropertyName);

        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }

    [Fact]
    public void StructProperty_ShouldReturnStructProperty_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.StructProperty(x => x.RequiredStructProperty);
    
        //Assert
        builder.ShouldBeOfType<StructProperty<Parameters, int>>();
    }
    
    [Fact]
    public void StructProperty_ShouldAddPropertyToList_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.StructProperty(x => x.RequiredStructProperty);
    
        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }
    
    [Fact]
    public void StructProperty_ShouldPushPropertyNameToNameStack_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => x.RequiredStructProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredStructProperty"));
    }
    
    [Fact]
    public void StructProperty_ShouldPushCustomNameToNameStack_OnRequiredStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => x.RequiredStructProperty, PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }
    
    [Fact]
    public void StructProperty_ShouldReturnStructProperty_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.StructProperty(x => x.OptionalStructProperty);
    
        //Assert
        builder.ShouldBeOfType<StructProperty<Parameters, int>>();
    }
    
    [Fact]
    public void StructProperty_ShouldAddPropertyToList_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.StructProperty(x => x.OptionalStructProperty);
    
        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }
    
    [Fact]
    public void StructProperty_ShouldPushPropertyNameToNameStack_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => x.OptionalStructProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("OptionalStructProperty"));
    }
    
    [Fact]
    public void StructProperty_ShouldPushCustomNameToNameStack_OnOptionalStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty(x => x.OptionalStructProperty, PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }
    
    [Fact]
    public void StructProperty_ShouldPushCustomNameToNameStack_OnManualTypedStructProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.StructProperty<int>(PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }
    
    [Fact]
    public void EnumProperty_ShouldReturnEnumProperty_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty);
    
        //Assert
        builder.ShouldBeOfType<EnumProperty<Parameters, TestEnum>>();
    }
    
    [Fact]
    public void EnumProperty_ShouldAddPropertyToList_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty);
    
        //Assert
        propertyBuilder.Properties.ShouldContain(builder);
    }

    [Fact]
    public void EnumProperty_ShouldPushPropertyNameToNameStack_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
        
        //Act
        _ = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty);
        
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredEnumProperty"));
    }
    
    [Fact]
    public void EnumProperty_ShouldPushCustomNameToNameStack_OnRequiredEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
        
        //Act
        _ = propertyBuilder.EnumProperty(x => x.RequiredEnumProperty, PropertyName);
        
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }
    
    [Fact]
    public void EnumProperty_ShouldReturnEnumProperty_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty);
    
        //Assert
        builder.ShouldBeOfType<EnumProperty<Parameters, TestEnum>>();
    }
    
    [Fact]
    public void EnumProperty_ShouldAddPropertyToList_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty);
    
        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }
    
    [Fact]
    public void EnumProperty_ShouldPushPropertyNameToNameStack_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("OptionalEnumProperty"));
    }
    
    [Fact]
    public void EnumProperty_ShouldPushCustomNameToNameStack_OnOptionalEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.EnumProperty(x => x.OptionalEnumProperty, PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }
    
    [Fact]
    public void EnumProperty_ShouldPushCustomNameToNameStack_OnManualTypedEnumProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
        
        //Act
        _ = propertyBuilder.EnumProperty<TestEnum>(PropertyName);
        
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }
    
    [Fact]
    public void ListProperty_ShouldReturnListProperty_OnListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.ListProperty(x => x.RequiredListProperty);
    
        //Assert
        builder.ShouldBeOfType<ListProperty<Parameters, int>>();
    }
    
    [Fact]
    public void ListProperty_ShouldAddPropertyToList_OnListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        var builder = propertyBuilder.ListProperty(x => x.RequiredListProperty);
    
        //Assert
        propertyBuilder.Properties.Count.ShouldBe(1);
        propertyBuilder.Properties.ShouldContain(builder);
    }
    
    [Fact]
    public void ListProperty_ShouldPushPropertyNameToNameStack_OnListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.ListProperty(x => x.RequiredListProperty);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry("RequiredListProperty"));
    }
    
    [Fact]
    public void ListProperty_ShouldPushCustomNameToNameStack_OnListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.ListProperty(x => x.RequiredListProperty, PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }
    
    [Fact]
    public void ListProperty_ShouldPushCustomNameToNameStack_OnManualTypedListProperty()
    {
        //Arrange
        var parameters = new Parameters();
        var nameStack = new NamingStack("");
        var propertyBuilder = new TestablePropertyBuilder(parameters, nameStack);
    
        //Act
        _ = propertyBuilder.ListProperty<int>(PropertyName);
    
        //Assert
        nameStack.Pop().ShouldBe(new PropertyNameEntry(PropertyName));
    }
}
