using System.Collections.ObjectModel;

namespace CleanDomainValidation.Domain;

/// <summary>
/// Read only collection of errors
/// </summary>
public sealed class ReadOnlyErrorCollection : ReadOnlyCollection<Error>
{
	internal ReadOnlyErrorCollection(IList<Error> list) : base(list) { }
}
