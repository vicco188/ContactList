using ContactListCommon.Interfaces;
using ContactListCommon.Services;
using ContactListConsole.Interfaces;
using ContactListConsole.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactListConsole;

internal class Program
{
	static void Main(string[] args)
	{
		string filePath = @"1345/contactlist.json";
		var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
		{
			services.AddScoped<IContactService, ContactService>();
			services.AddScoped<IConsoleInterfaceService, ConsoleInterfaceService>();
			services.AddScoped<IFileService>(provider => new FileService(filePath));
		}).Build();
		builder.Start();
		IConsoleInterfaceService consoleInterfaceService = builder.Services.GetRequiredService<IConsoleInterfaceService>();
		consoleInterfaceService.ShowMainMenu();
	}
}
