using ContactListCommon.Enumerations;
using ContactListCommon.Interfaces;
using ContactListCommon.Models.Responses;
using Newtonsoft.Json;

namespace ContactListCommon.Services;

/// <summary>
/// A service handling a list of contacts and the interactions with this list
/// </summary>
public class ContactService : IContactService
{
	private readonly List<IContact> contactList;
	private readonly IFileService fileService;

	/// <summary>
	/// A service handling a list of contacts and the interactions with this list
	/// </summary>
	/// <param name="fileService">An IFileService object to handle interactions with a save file for the contact list</param>
	public ContactService(IFileService fileService)
	{
		this.fileService = fileService;
		try
		{
			string fileContent = fileService.ReadFromFile();
			contactList = JsonConvert.DeserializeObject<List<IContact>>(fileContent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects })!;
		}
		catch (Exception ex)
		{
			contactList = new List<IContact>();
			Console.WriteLine(ex.Message);
		}
	}

	public IServiceResponse AddContact(IContact contact)
	{
		if (!contactList.Any(x => x.Email == contact.Email.ToLower()))
		{
			try
			{
				contactList.Add(contact);
				fileService.WriteToFile(JsonConvert.SerializeObject(contactList, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Objects,
					Formatting = Formatting.Indented
				}));
				return new ServiceResponse(contact, ResponseStatus.SUCCESS);
			}
			catch (Exception ex)
			{
				return new ServiceResponse("Could not add contact: " + ex.Message, ResponseStatus.FAILED);
			}

		}
		else
		{
			return new ServiceResponse("Contact already exists", ResponseStatus.FAILED);
		}

	}

	public IServiceResponse GetContact(string email)
	{
		IServiceResponse response = new ServiceResponse();
		IContact contact = contactList.FirstOrDefault(contact => contact.Email == email.ToLower())!;
		if (contact != null)
		{
			try
			{
				response.Result = contact;
				response.Status = ResponseStatus.SUCCESS;
			}
			catch (Exception ex)
			{
				response.Result = "Could not load contact: " + ex.Message;
				response.Status = ResponseStatus.FAILED;
			}
		}
		else
		{
			response.Result = "Contact not found";
			response.Status = ResponseStatus.FAILED;
		}
		return response;


	}
		public IServiceResponse GetAllContacts()
	{
		try
		{
			if (contactList.Count > 0)
			{
				IEnumerable<IContact> contacts = contactList;
				return new ServiceResponse(contacts, ResponseStatus.SUCCESS);
			}
			else
			{
				return new ServiceResponse("Contact list is empty", ResponseStatus.FAILED);
			}
		}
		catch
		{
			return new ServiceResponse("Contact list is empty", ResponseStatus.FAILED);
		}

	}


	public IServiceResponse DeleteContact(string email)
	{
		if (contactList.Any(x => x.Email == email.ToLower()))
		{
			IContact contact = contactList.FirstOrDefault(x => x.Email.ToLower() == email.ToLower())!;
			try
			{
				contactList.Remove(contact);
				fileService.WriteToFile(JsonConvert.SerializeObject(contactList, new JsonSerializerSettings
				{
					TypeNameHandling = TypeNameHandling.Objects,
					Formatting = Formatting.Indented
				}));
				return new ServiceResponse(contact, ResponseStatus.SUCCESS);
			}
			catch (Exception ex)
			{
				return new ServiceResponse("Could not remove contact: " + ex.Message, ResponseStatus.FAILED);
			}
		}
		else
		{
			return new ServiceResponse("Contact not found", ResponseStatus.FAILED);
		}
	}
}
