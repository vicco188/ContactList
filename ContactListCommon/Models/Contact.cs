using ContactListCommon.Interfaces;

namespace ContactListCommon.Models;

public class Contact : IContact
{
	private string email = null!;

	public Guid Id { get; set; }
	public DateTime DateCreated { get; set; }

	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email
	{
		get { return email; }
		set { email = value.ToLower(); }
	}
	public string Phone { get; set; } = null!;
	public string StreetAddress { get; set; } = null!;
	public string City { get; set; } = null!;
	public string PostalCode { get; set; } = null!;


	public Contact(string firstName, string lastName, string email, string phone, string streetAddress, string postalCode, string city)
	{
		Id = Guid.NewGuid();
		FirstName = firstName;
		LastName = lastName;
		Email = email;
		Phone = phone;
		StreetAddress = streetAddress;
		PostalCode = postalCode;
		City = city;
		DateCreated = DateTime.Now;
	}

	public Contact()
	{
		Id = Guid.NewGuid();
		DateCreated = DateTime.Now;
	}

	public override string ToString()
	{
		return $"{FirstName} {LastName} <{Email}>";
	}
}
