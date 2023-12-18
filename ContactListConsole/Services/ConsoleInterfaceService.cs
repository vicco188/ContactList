using ContactListCommon.Enumerations;
using ContactListCommon.Interfaces;
using ContactListCommon.Models;
using ContactListConsole.Interfaces;

namespace ContactListConsole.Services;

internal class ConsoleInterfaceService(IContactService contactService) : IConsoleInterfaceService
{
	private IContactService _contactService = contactService;


	public void ShowMainMenu()
	{
		while (true)
		{
			Console.Clear();
			Console.WriteLine("1. Add 2. Show All 3. Delete contact 0. Exit");
			Console.Write("Val: ");
			string choice = Console.ReadLine()!;
			Console.Clear();
			switch (choice)
			{
				case "1": ShowAddMenu(); break;
				case "2": ShowAllMenu(); break;
				case "3": ShowRemoveByEmailMenu(); break;
				case "0": Environment.Exit(0); break;
			}
		}
	}

	/// <summary>
	/// Interacts with the user to add a contact to the contact list
	/// </summary>
	private void ShowAddMenu()
	{
		Console.Write("Förnamn: ");
		string firstName = Console.ReadLine() ?? string.Empty;
		Console.Write("Efternamn: ");
		string lastName = Console.ReadLine() ?? string.Empty;

		string email;
		while (true)
		{
			Console.Write("Epost: ");
			email = Console.ReadLine()!;
			if (!string.IsNullOrEmpty(email)) { break; }
			Console.WriteLine("Epostadress är obligatorisk!");
		}
		Console.Write("Telefonnummer: ");
		string phone = Console.ReadLine() ?? string.Empty;
		Console.Write("Gatuadress: ");
		string address = Console.ReadLine() ?? string.Empty;
		Console.Write("Postnummer: ");
		string postalCode = Console.ReadLine() ?? string.Empty;
		Console.Write("Postort: ");
		string city = Console.ReadLine() ?? string.Empty;


		IContact contact = new Contact(firstName, lastName, email, phone, address, postalCode, city);

		var res = _contactService.AddContact(contact);
		if ((int)res.Status == 1)
		{
			Console.WriteLine($"{contact} lades till");
		}
		else
		{
			Console.WriteLine($"Kunde inte lägga till kontakt. Felmeddelande: {res.Result}");
		}
		PressAnyKey();

	}

	/// <summary>
	/// Interacts with the user to remove a contact from the contact list
	/// </summary>
	private void ShowRemoveByEmailMenu()
	{
		Console.Write("Email to remove: ");
		string email = Console.ReadLine()!;
		var res = _contactService.DeleteContact(email);
		if (res.Status == ResponseStatus.SUCCESS)
		{
			Console.WriteLine($"Kontakten [{res.Result}] har tagits bort");
		}
		else
		{
			Console.WriteLine($"Kunde inte ta bort. Felmeddelande: {res.Result}");
		}
		PressAnyKey();
	}

	/// <summary>
	/// Shows all contacts in list and interacts with the user to either remove or show details for a position in the list
	/// </summary>
	private void ShowAllMenu()
	{
		var res = _contactService.GetAllContacts();
		if (res.Status == ResponseStatus.SUCCESS)
		{
			IEnumerable<IContact>? contactList = res.Result as IEnumerable<IContact>;
			Console.WriteLine("KONTAKTLISTA\n============");
			int i = 1;
			foreach (var contact in contactList!)
			{
				Console.WriteLine($"{i}. {contact}>");
				i++;
			}
			Console.WriteLine("============");
			Console.WriteLine("1. Show details 2. Delete contact 0. Main menu");
			Console.Write("Val: ");
			string choice = Console.ReadLine()!;
			switch (choice)
			{
				case "1": ShowViewDetailsByNumberMenu(contactList); break;
				case "2": ShowRemoveByNumberMenu(contactList); break;
				case "3": Console.Clear(); break;
			}
		}
		else
		{
			Console.WriteLine("Listan är tom");
		}
	}

	/// <summary>
	/// Interacts with the user to show the details for a specific contact in a contact list
	/// </summary>
	/// <param name="contactList">A IEnumerable of IContacts in which the contact to show details for exists</param>
	private void ShowViewDetailsByNumberMenu(IEnumerable<IContact> contactList)
	{
		Console.Write("Ange numret i listan på den kontakt du se: ");

		if (int.TryParse(Console.ReadLine(), out int choice))
		{
			if (choice <= contactList.Count() && choice > 0)
			{
				Console.Clear();
				IContact contact = contactList.ElementAt(choice - 1);
				Console.WriteLine("KONTAKT\n=======");
				Console.WriteLine($"ID:            {contact.Id}");
				Console.WriteLine($"Förnamn:       {contact.FirstName}");
				Console.WriteLine($"Efternamn:     {contact.LastName}");
				Console.WriteLine($"Epost:         {contact.Email}");
				Console.WriteLine($"Telefonnummer: {contact.Phone}");
				Console.WriteLine($"Adress:        {contact.StreetAddress}, {contact.PostalCode} {contact.City}");
				Console.WriteLine($"Tillagd:       {contact.DateCreated.ToString("yyyy-MM-dd HH:mm")}");
				Console.WriteLine("=======");
				PressAnyKey();

			}
			else
			{
				Console.WriteLine("Ogiltigt nummer");
				PressAnyKey();
			}
		}
	}

	/// <summary>
	/// Interacts with the user to delete specific contact from the contact list
	/// </summary>
	/// <param name="contactList">A IEnumerable of IContacts in which the contact to delete from the global list exists</param>
	private void ShowRemoveByNumberMenu(IEnumerable<IContact> contactList)
	{
		Console.Write("Ange numret i listan på den kontakt du vill ta bort: ");

		if (int.TryParse(Console.ReadLine(), out int choice))
		{
			if (choice <= contactList.Count() && choice > 0)
			{
				Console.Write($"Du vill ta bort {contactList.ElementAt(choice - 1)} (y/n)? ");
				string confirmation = Console.ReadLine()!;
				if (string.Equals(confirmation, "y", StringComparison.OrdinalIgnoreCase))
				{
					var result = _contactService.DeleteContact(contactList.ElementAt(choice - 1).Email);
					if (result.Status == ResponseStatus.SUCCESS)
					{
						Console.WriteLine($"{result.Result} borttagen");
						PressAnyKey();
					}
					else
					{
						Console.WriteLine($"Borttagning misslyckades. Felmeddelande: {result.Result}");
						PressAnyKey();
					}
				}

			}
			else
			{
				Console.WriteLine("Ogiltigt nummer");
			}
		}
	}

	/// <summary>
	/// Will pause the excecution of the program pending user key press then clears console
	/// </summary>
	private void PressAnyKey()
	{
		Console.Write("\nTryck någon tangent för att fortsätta . . .");
		Console.ReadKey();
		Console.Clear();
	}
}
