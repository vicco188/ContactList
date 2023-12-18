using ContactListCommon.Interfaces;
using ContactListCommon.Services;

namespace ContactListCommon.Test;

public class FileService_Tests
{

	[Fact]

	public void WriteToFileThenReadFromFile_ShouldReturnSameContent_IfFolderExists()
	{
		// Arrange
		string filePath = "TestFile-" + Guid.NewGuid().ToString() + ".txt"; // will create new file in current folder, which evidently exists
		IFileService fileService = new FileService(filePath);
		string content = Guid.NewGuid().ToString();

		// Act
		fileService.WriteToFile(content);
		string result = fileService.ReadFromFile();

		// Assert
		Assert.Equal(content, result);
	}

	[Fact]
	public void WriteToFileThenReadFromFile_ShouldReturnEmptyList_IfFileIsBlank()
	{
		// Arrange
		string filePath = "TestFile-" + Guid.NewGuid().ToString() + ".txt"; // will create new file in current folder, which evidently exists
		IFileService fileService = new FileService(filePath);
		string content = "";

		// Act
		fileService.WriteToFile(content);
		string result = fileService.ReadFromFile();

		// Assert
		Assert.Equal("[]", result);
	}

	[Fact]
	public void ReadFromFile_ShouldReturnEmptyList_IfFolderDoesNotExist()
	{
		// Arrange
		string filePath = "TestFolder-" + Guid.NewGuid().ToString() + @"\" + Guid.NewGuid().ToString() + ".txt"; // will use a file path to a non-existent folder
		IFileService fileService = new FileService(filePath);

		// Act
		string result = fileService.ReadFromFile();

		// Assert
		Assert.Equal("[]", result);
	}

	[Fact]
	public void WriteToFile_ShouldThrowException_IfFolderDoesNotExist()
	{
		// Arrange
		string filePath = "TestFolder-" + Guid.NewGuid().ToString() + @"\" + Guid.NewGuid().ToString() + ".txt"; // will use a file path to a non-existent folder
		IFileService fileService = new FileService(filePath);
		bool exceptionIsThrown = false;
		string content = Guid.NewGuid().ToString();

		// Act
		try
		{
			fileService.WriteToFile(content);
		} 
		catch
		{
			exceptionIsThrown = true;
		}

		// Assert 
		Assert.True(exceptionIsThrown);

	}
}
