using AdventOfCode2022.Common;

namespace AdventOfCode2022.Day05;

public class Instruction
{
    public int From { get; init; }
    public int To { get; init; }
    public int MoveNumber { get; init; }
}

public class StackContainer
{
    public List<Stack<char>> Stacks { get; set; }

    public StackContainer()
    {
        Stacks = new List<Stack<char>>();
    }

    public void Process9001Instruction(Instruction instruction)
    {
        var tempStack = new Stack<char>();
        for (var i = 0; i < instruction.MoveNumber; i++)
        {
            var item = Stacks[instruction.From].Pop();
            tempStack.Push(item);
        }
        for (var i = 0; i < instruction.MoveNumber; i++)
        {
            var item = tempStack.Pop();
            Stacks[instruction.To].Push(item);
        }
    }
    
    public void Process9000Instruction(Instruction instruction)
    {
        for (var i = 0; i < instruction.MoveNumber; i++)
        {
            var item = Stacks[instruction.From].Pop();
            Stacks[instruction.To].Push(item);
        }
    }

    public string GetTopLine()
    {
        return Stacks.Aggregate("", (s, chars) => s + chars.Peek());
    }
}

public class Day05
{
    private readonly StackContainer stackContainer = new ();
    public Day05()
    {
        var lines = FileReader.ReadAllLines("Day5Initial.txt");
        InitialiseStacks(lines);
        FileReader
            .ReadAllLines("Day5Input.txt")
            .Select(CreateInstruction)
            .ToList()
            .ForEach(i => stackContainer.Process9001Instruction(i));
        
        Console.WriteLine(stackContainer.GetTopLine());
    }

    private void InitialiseStacks(List<string> lines)
    {
        lines.ForEach(line =>
        {
            var chars = line.ToCharArray().ToList();
            var stack = new Stack<char>();
            chars.ForEach(c => stack.Push(c));
            stackContainer.Stacks.Add(stack);
        });
    }

    private static Instruction CreateInstruction(string input)
    {
        var sizeString = input.Split(" from ")[0];
        var locationString = input.Split(" from ")[1];
        return new Instruction
        {
            From = int.Parse(locationString.Split(" to ")[0]) - 1,
            To = int.Parse(locationString.Split(" to ")[1]) - 1,
            MoveNumber = int.Parse(sizeString.Split(" ")[1]),
        };
    }
}