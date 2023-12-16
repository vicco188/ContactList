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
	string PostalCode { get; set; } // string and not uint or similar in case any forms of postal codes contain initial zeros and/or letters
}
