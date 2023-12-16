using ContactListCommon.Enumerations;

namespace ContactListCommon.Interfaces;

/// <summary>
/// A model for a service response that contains the status of an operation and an additional return object
/// </summary>
public interface IServiceResponse
{
	
	object Result { get; set; }
	ResponseStatus Status { get; set; }
}
