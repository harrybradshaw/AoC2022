namespace AdventOfCode2022.Common;

public static class FileReader
{
    private const string BasePath = @"C:\Work\Training\AdventOfCode2022\AdventOfCode2022\Inputs";
    public static string ReadAllText(string filePath)
    {
        return File.ReadAllText($"{BasePath}/{filePath}");
    }

    public static List<string> ReadAllLines(string filePath)
    {
        return File.ReadAllLines($"{BasePath}/{filePath}")
            .ToList();
    }
}