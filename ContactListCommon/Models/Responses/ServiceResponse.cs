using ContactListCommon.Enumerations;
using ContactListCommon.Interfaces;

namespace ContactListCommon.Models.Responses;

/// <summary>
/// A model for a service response that contains the status of an operation and an additional return object
/// </summary>
public class ServiceResponse : IServiceResponse
{
	public object Result { get; set; }
	public ResponseStatus Status { get; set; }
	public ServiceResponse(object result, ResponseStatus status)
	{
		Result = result;
		Status = status;
	}
	public ServiceResponse()
	{
		Result = null!;
	}
}
