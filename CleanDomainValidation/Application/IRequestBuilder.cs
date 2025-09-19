namespace CleanDomainValidation.Application;

/// <summary>
/// Builder for validating <typeparamref name="TParameters"/> and mapping it to a request of type <typeparamref name="TRequest"/>
/// </summary>
public interface IRequestBuilder<TParameters, TRequest>
	where TRequest : IRequest

{
	/// <summary>
	/// Configures the required properties
	/// </summary>
	ValidatedRequiredProperty<TRequest> Configure(RequiredPropertyBuilder<TParameters, TRequest> builder);
}
