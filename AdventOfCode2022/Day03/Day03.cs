using AdventOfCode2022.Common;

namespace AdventOfCode2022.Day03;

public class Day03
{
    public Day03()
    {
        var lines = FileReader.ReadAllLines("Day3Input.txt");
        var sum = CalculateBadgeSum(lines);
        Console.WriteLine(sum);
    }

    private int CalculateBadgeSum(List<string> lines)
    {
        var numPages = lines.Count / 3;
        var sum = 0;
        for (var i = 0; i < numPages; i++)
        {
            var page = CreatePage(lines, pageNumber: i);
            sum += CalculatePageValue(page);
        }

        return sum;
    }

    private List<string> CreatePage(List<string> lines, int pageNumber)
    {
        const int pageLength = 3;
        return lines
            .Skip(pageNumber * pageLength)
            .Take(pageLength)
            .ToList();
    }

    private static int CalculatePageValue(List<string> page)
    {
        var sets = page.Select(line => line.ToHashSet()).ToList();
        var output = sets.Skip(1).Aggregate(sets[0], (accSet, chars) =>
        {
            accSet.IntersectWith(chars);
            return accSet;
        });
        return CalculatePriorityFromSet(output);
    }

    private static int CalculateComponentSum(List<string> lines)
    {
        return lines.Aggregate(0, (i, s) =>
        {
            var set = CalculateIntersectionSet(s);
            Console.WriteLine(CalculatePriorityFromSet(set));
            return i + CalculatePriorityFromSet(set);
        });
    }

    private static HashSet<char> CalculateIntersectionSet(string inputString)
    {
        var length = inputString.Length / 2;
        var firstHalf = inputString[..length].ToHashSet();
        var secondHalf = inputString[length..].ToHashSet();

        firstHalf.IntersectWith(secondHalf);
        return firstHalf;
    }

    private static int CalculatePriorityFromSet(HashSet<char> set)
    {
        var item = set.ToArray()[0];
        if (item > 96)
        {
            return item - 96;
        }
        return item - 65 + 27;
    }
}