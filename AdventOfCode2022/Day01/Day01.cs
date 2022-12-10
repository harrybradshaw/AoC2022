using AdventOfCode2022.Common;

namespace AdventOfCode2022.Day01;

public class Day01
{
    public Day01()
    {
        var calorieSums = FileReader.ReadAllText("Day1Input.txt")
            .Split("\r\n\r\n")
            .Select(s => s.Split("\r\n")
                .Select(decimal.Parse)
                .Sum())
            .OrderDescending()
            .ToList();
        Console.WriteLine(calorieSums.Take(3).Sum());
    }
}