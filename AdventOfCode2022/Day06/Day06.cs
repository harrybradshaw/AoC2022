using AdventOfCode2022.Common;

namespace AdventOfCode2022.Day06;

public class Day06
{
    private readonly Queue<char> _queue = new ();
    private const int QueueSize = 14;

    public Day06()
    {
        var chars = FileReader
            .ReadAllText("Day6Test.txt")
            .ToCharArray();

        for (var i = 0; i < chars.Length; i++)
        {
            var c = chars[i];
            var markerIndex = ProcessCharacter(c, i);
            if (markerIndex != null)
            {
                Console.WriteLine(markerIndex);
                return;
            };
        }
    }

    private int? ProcessCharacter(char c, int i)
    {
        _queue.Enqueue(c);
        if (_queue.Count > QueueSize)
        {
            _queue.Dequeue();
            if (_queue.ToHashSet().Count == _queue.Count)
            {
                return i + 1;
            }
        }

        return null;
    }
}