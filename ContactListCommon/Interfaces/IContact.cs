namespace ContactListCommon.Interfaces;
/// <summary>
/// Model of an object that contains a contact's name, email, address and phone number
/// </summary>
public interface IContact
{
	Guid Id { get; }
	DateTime DateCreated { get; }
	string FirstName { get; set; }
	string LastName { get; set; }
	string Email { get; set; }
	string Phone { get; set; }
	string StreetAddress { get; set; }
	string City { get; set; }
	string PostalCode { get; set; } // string and not uint or similar in in order to allow postal codes with initial zeros and/or letters
}
