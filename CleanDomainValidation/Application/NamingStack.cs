namespace CleanDomainValidation.Application;

public class NamingStack
{
    private Stack<INameStackEntry> _propertyNamesStack;
    private readonly string _prefix;
    
    internal NamingStack(string prefix)
    {
        _prefix = prefix;
        _propertyNamesStack = [];
    }

    public string ErrorCode => GenerateErrorCode();

    public string ErrorMessage => GenerateErrorMessage();

    internal void PushProperty(string name)
    {
        _propertyNamesStack.Push(new PropertyNameEntry(name));
    }

    internal void PushIndex(int index)
    {
        _propertyNamesStack.Push(new IndexEntry(index));
    }

    internal void Pop()
    {
        _propertyNamesStack.Pop();
    }

    private string GenerateErrorCode()
    {
        var result = _prefix;
        if (result.Length > 0) result += ".";
        return result + string.Join(".", _propertyNamesStack
            .Reverse()
            .OfType<PropertyNameEntry>()
            .Select(e => e.Name)) + ".Missing";
    }

    private string GenerateErrorMessage()
    {
        var lastPropertyName = (PropertyNameEntry)_propertyNamesStack.Peek();

        return$"Property \"{lastPropertyName.Name}\" is required but missing or null in the request.";
    }
}