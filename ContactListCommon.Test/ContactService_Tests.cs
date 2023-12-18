using ContactListCommon.Enumerations;
using ContactListCommon.Interfaces;
using ContactListCommon.Models;
using ContactListCommon.Services;
using Moq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ContactListCommon.Test;

public class ContactService_Tests
{
	private string mockString;
	public ContactService_Tests() 
	{
		List<IContact> ctList = [];
		ctList.Add(new Contact("Test", "Test", "test@test.test", "1234567890", "Street 1", "12345", "City"));
		ctList.Add(new Contact("Test2", "Test2", "test2@test2.test", "2345678901", "Street 2", "12345", "City"));
		mockString = JsonConvert.SerializeObject(ctList, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
	}


	[Fact]
	public void GetContact_ShouldReturnContactObject_IfEmailExists()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		mockFileService.Setup(x => x.ReadFromFile()).Returns(mockString);
		IContactService contactService = new ContactService(mockFileService.Object);
		string email = "test@test.test";
		// Act
		var result = contactService.GetContact(email);
		// Assert
		Assert.Equal(ResponseStatus.SUCCESS, result.Status);
		Assert.Equal(email, (result.Result as IContact)!.Email);
		Assert.True(result.Result is IContact);
	}

	[Fact]
	public void GetContact_ShouldReturnFailed_IfEmailDoesntExist()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		mockFileService.Setup(x => x.ReadFromFile()).Returns(mockString);
		IContactService contactService = new ContactService(mockFileService.Object);
		string email = Guid.NewGuid().ToString();
		// Act
		var result = contactService.GetContact(email);
		// Assert
		Assert.Equal(ResponseStatus.FAILED, result.Status);
		Assert.True(result.Result is String);
		Assert.Equal("Contact not found", (string)result.Result);
	}

	[Fact]
	public void GetContact_ShouldReturnFailed_IfContactListIsEmpty()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		mockFileService.Setup(x => x.ReadFromFile()).Returns("[]");
		IContactService contactService = new ContactService(mockFileService.Object);
		string email = "test@test.test";
		// Act
		var result = contactService.GetContact(email);
		// Assert
		Assert.Equal(ResponseStatus.FAILED, result.Status);
		Assert.True(result.Result is String);
		Assert.Equal("Contact not found", (string)result.Result);
	}


	[Fact]
	public void AddContact_ShouldReturnSuccess_IfListIsEmpty()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		mockFileService.Setup(x => x.ReadFromFile()).Returns("[]");
		IContactService contactService = new ContactService(mockFileService.Object);
		IContact contact = new Contact("Test", "Test", "test@test.test", "1234567890", "Street 1", "12345", "City");
		// Act
		var result = contactService.AddContact(contact);
		// Assert
		Assert.Equal(ResponseStatus.SUCCESS, result.Status);
		Assert.True(result.Result is IContact);
		Assert.Equal("test@test.test", (result.Result as Contact)!.Email);
	}
	[Fact]
	public void AddContact_ShouldReturnFailure_IfContactExists()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		IContact contact = new Contact("Test", "Test", "test@test.test", "1234567890", "Street 1", "12345", "City");
		mockFileService.Setup(x => x.ReadFromFile()).Returns(mockString);
		IContactService contactService = new ContactService(mockFileService.Object);
		// Act
		var result = contactService.AddContact(contact);
		// Assert
		Assert.Equal(ResponseStatus.FAILED, result.Status);
		Assert.Equal("Contact already exists", (string)result.Result);
	}

	[Fact]
	public void AddContact_ShouldReturnSuccess_IfEmailDoesntExist()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		mockFileService.Setup(x => x.ReadFromFile()).Returns(mockString);
		IContactService contactService = new ContactService(mockFileService.Object);
		IContact contact = new Contact("Test", "Test", "new@new.new", "1234567890", "Street 1", "12345", "City");
		// Act
		var result = contactService.AddContact(contact);
		// Assert
		Assert.Equal(ResponseStatus.SUCCESS, result.Status);
		Assert.Equal(contact, result.Result as Contact);
	}

	[Fact]
	public void GetAllContacts_ShouldReturnSuccess_IfListIsNotEmpty()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		mockFileService.Setup(x => x.ReadFromFile()).Returns(mockString);
		IContactService contactService = new ContactService(mockFileService.Object);

		// Act
		var result = contactService.GetAllContacts();

		//Assert
		Assert.True(result.Result is IEnumerable<IContact>);
		Assert.Equal(ResponseStatus.SUCCESS, result.Status);
		Assert.Contains((result.Result as IEnumerable<IContact>)!, x => x.Email == "test@test.test");
	}

	[Fact] 
	public void GetAllContacts_ShouldReturnFailed_IfListIsEmpty()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		mockFileService.Setup(x => x.ReadFromFile()).Returns("[]");
		IContactService contactService = new ContactService(mockFileService.Object);

		// Act
		var result = contactService.GetAllContacts();

		// Assert
		Assert.Equal(ResponseStatus.FAILED, result.Status );
		Assert.Equal("Contact list is empty", (string)result.Result);
	}

	[Fact]
	public void DeleteContact_ShouldDeleteContact_IfContactExists()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		mockFileService.Setup(x => x.ReadFromFile()).Returns(mockString);
		IContactService contactService = new ContactService(mockFileService.Object);

		// Act
		IServiceResponse result = contactService.DeleteContact("test@test.test");

		// Assert
		Assert.Equal(ResponseStatus.SUCCESS, result.Status);
		Assert.True(result.Result is IContact);
	}

	[Fact]
	public void DeleteContact_ShouldReturnedFailed_IfContactDoesntExist()
	{
		// Arrange
		var mockFileService = new Mock<IFileService>();
		mockFileService.Setup(x => x.ReadFromFile()).Returns(mockString);
		IContactService contactService = new ContactService(mockFileService.Object);

		// Act
		IServiceResponse result = contactService.DeleteContact("new@new.new");

		// Assert
		Assert.Equal(ResponseStatus.FAILED, result.Status);
		Assert.Equal("Contact not found", (string)result.Result);
	}
}
