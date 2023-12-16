namespace ContactListCommon.Interfaces;

/// <summary>
/// A service handling a list of contacts and the interactions with this list
/// </summary>
public interface IContactService
{

	/// <summary>
	/// Adds new contact to contact list
	/// </summary>
	/// <param name="contact">The contact to be added</param>
	/// <returns>An IServiceResponse containing the status of the operation (as .Status) and if successful the contact object itself (as .Result) or otherwise an error message string (as .Result)</returns>
	IServiceResponse AddContact(IContact contact);

	/// <summary>
	/// Retrieves a contact from the contact list based on the provided email.
	/// </summary>
	/// <param name="email">The email of the contact to be retrieved.</param>
	/// <returns>An IServiceResponse containing the status of the operation (as .Status) and if successful the contact requested (as .Result) or otherwise an error message string (as .Result).</returns>
	IServiceResponse GetContact(string email);

	/// <summary>
	/// Removes a contact from the contact list
	/// </summary>
	/// <param name="email">The email of the contact to delete</param>
	/// <returns>An IServiceResponse containing the status of the operation (as .Status) and if the successful the removed contact (as .Result) or otherwise an error message string (as .Result)</returns>
	IServiceResponse DeleteContact(string email);
	
	/// <summary>
	/// Retrieves an IEnumerable with all contacts in list
	/// </summary>
	/// <returns>An IServiceResponse containing the status of the operation (as .Status) and if successful an IEnumerble with all contacts (as .Result) or otherwise an error message string (as .Result)</returns>
	IServiceResponse GetAllContacts();
	
}
