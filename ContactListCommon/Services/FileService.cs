using ContactListCommon.Interfaces;
using System.Diagnostics;

namespace ContactListCommon.Services;

/// <summary>
/// A Service which handles writing to or reading from a specified file path
/// </summary>
/// <param name="filePath">The file path to the file to be read from and written to</param>
public class FileService(string filePath) : IFileService
{
	private readonly string _filePath = filePath;

	public void WriteToFile(string content)
	{
		try
		{
			using (StreamWriter writer = new StreamWriter(_filePath, false))
			{
				writer.WriteLine(content);
			}
		}
		catch
		{
			throw; // pass on the exception to calling method
		}

	}


	public string ReadFromFile()
	{
		string result = "[]"; 

		if (File.Exists(_filePath))
		{
			try
			{
				using (StreamReader reader = new StreamReader(_filePath))
				{
					result = reader.ReadToEnd().Trim();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error reading file: {ex.Message}");
			}
			if (string.IsNullOrEmpty(result))
			{
				result = "[]"; // blank file will return empty string
			}
		}
		
		return result;
	}
}
