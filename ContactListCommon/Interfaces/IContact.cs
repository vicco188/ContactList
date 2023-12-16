namespace ContactListCommon.Interfaces;

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
