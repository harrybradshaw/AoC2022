using AdventOfCode2022.Common;

namespace AdventOfCode2022.Day04;

public class Range
{
    public int Start { get; set; }
    public int End { get; set; }

    public Range(string input)
    {
        Start = int.Parse(input.Split('-')[0]);
        End = int.Parse(input.Split('-')[1]);
    }

    public bool FullyContains(Range range)
    {
        return (range.Start >= Start && range.Start <= End) &&
               (range.End >= Start && range.End <= End);
    }

    public bool Contains(Range range)
    {
        return range.Start <= End;
    }
}

public class Day04
{
    public Day04()
    {
        var lines = FileReader.ReadAllLines("Day4Input.txt");
        var contains = lines.Where(line =>
        {
            var splitString = line.Split(',');
            var range1 = new Range(splitString[0]);
            var range2 = new Range(splitString[1]);
            return range1.Contains(range2) && range2.Contains(range1);
        });
        Console.WriteLine(contains.Count());
    }
    
    private bool CheckRangesFullyOverlap(Range a, Range b) =>
        a.FullyContains(b) || b.FullyContains(a);
}