using ContactListCommon.Interfaces;

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
		using (StreamWriter writer = new StreamWriter(_filePath, false))
		{
			writer.WriteLine(content);
		}

	}

	public string ReadFromFile()
	{
		if (File.Exists(_filePath))
		{
			using (StreamReader reader = new StreamReader(_filePath))
			{
				string result = reader.ReadToEnd();

				if (string.IsNullOrEmpty(result))
					result = "[]"; // Existing but empty file will return empty list []
				return result;
			}
		}
		else
		{
			return "[]"; // Not existing file will return empty list []
		}
	}
}
