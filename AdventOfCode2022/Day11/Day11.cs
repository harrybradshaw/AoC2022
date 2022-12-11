namespace AdventOfCode2022.Day11;

public class Day11
{
    private readonly List<Monkey> _monkeys;
    private readonly long _commonMultipleOfTestFactors;

    private void ProcessRound()
    {
        _monkeys.ForEach(monkey =>
        {
            while (monkey.Items.Count != 0)
            {
                ProcessTopItem(monkey);
                monkey.InspectionCount += 1;
            }
        });
    }

    private void ProcessTopItem(Monkey monkey)
    {
        var item = monkey.Items.Dequeue();
        item = monkey.Operation(item);
        // Round 1 only
        // item /= 3;
        item %= _commonMultipleOfTestFactors;
        var target = item % monkey.TestDivisor == 0
            ? monkey.TrueTarget
            : monkey.FalseTarget;
        _monkeys[target].Items.Enqueue(item);
    }

    public Day11()
    {
        _monkeys = MonkeyFactory.CreateMonkeys();
        _commonMultipleOfTestFactors = _monkeys.Aggregate(1, (acc, m) => acc * m.TestDivisor);
        
        for (var i = 0; i < 10000; i++)
        {
            ProcessRound();
        }

        OutputMonkeyBusiness();
    }

    private void OutputMonkeyBusiness()
    { 
        var output = _monkeys
            .Select(monkey => monkey.InspectionCount)
            .OrderDescending()
            .Take(2)
            .Aggregate((long)1, (acc, val) => acc * val);
        Console.WriteLine(output);
    }
}

public class Monkey
{
    public Queue<long> Items { get; set; }
    public Func<long, long> Operation { get; set; }
    public int TestDivisor { get; set; }
    public int TrueTarget { get; set; }
    public int FalseTarget { get; set; }
    public long InspectionCount { get; set; }
}