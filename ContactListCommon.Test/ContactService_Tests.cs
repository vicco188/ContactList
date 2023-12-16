using ContactListCommon.Enumerations;
using ContactListCommon.Interfaces;
using ContactListCommon.Models;
using ContactListCommon.Services;

namespace ContactListCommon.Test;

public class ContactService_Tests
{
	[Fact]
	public void GetContact_ShouldReturnContactObject_IfEmailExists()
	{
		// Arrange
		IFileService fileService = new FileService("contactlist.json");
		IContactService contactService = new ContactService(fileService);
		string email = "vicco188@gmail.com";
		// Act
		var result = contactService.GetContact(email);
		// Assert
		Assert.Equal(ResponseStatus.SUCCESS, result.Status);
		Assert.Equal(email, (result.Result as IContact)!.Email);
		Assert.True(result.Result is IContact);
	}

	[Fact]
	public void GetContact_ShouldReturnFailure_IfEmailDoesntExist()
	{
		// Arrange
		IFileService fileService = new FileService("contactlist.json");
		IContactService contactService = new ContactService(fileService);
		string email = Guid.NewGuid().ToString();
		// Act
		var result = contactService.GetContact(email);
		// Assert
		Assert.Equal(ResponseStatus.FAILED, result.Status);
		Assert.True(result.Result is String);
	}

}
