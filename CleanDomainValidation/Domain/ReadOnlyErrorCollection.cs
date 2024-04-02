using System.Collections.ObjectModel;

namespace CleanDomainValidation.Domain;

public sealed class ReadOnlyErrorCollection : ReadOnlyCollection<Error>
{
	internal ReadOnlyErrorCollection(IList<Error> list) : base(list) { }
}
