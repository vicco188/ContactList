
namespace ContactListCommon.Interfaces;

/// <summary>
/// A Service which handles writing to or reading from a specified file path
/// </summary>
public interface IFileService
{

	/// <summary>
	/// Retrieves the text content from the list file
	/// </summary>
	/// <returns>The text from the file as a string if found, or otherwise an empty list as a json string []</returns>
	string ReadFromFile();

	/// <summary>
	/// Writes a specified string to the list file
	/// </summary>
	/// <param name="content">The string to write to the file</param>
	void WriteToFile(string content);
}
